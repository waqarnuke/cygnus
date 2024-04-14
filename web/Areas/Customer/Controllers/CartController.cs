using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Models.Identity;
using Utility.Common;
using Microsoft.AspNetCore.Identity.UI.Services;



namespace Web.Areas.Customer.Controllers;


[Area("Customer")]
[Authorize]
public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;
    private readonly IUnitOfWork _unitOfwork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailSender _emailSender;

    [BindProperty]
    public ShoppingCartVM ShoppingCartVM {get; set;}
    public CartController(ILogger<CartController> logger, IUnitOfWork unitOfWork, IEmailSender emailSender)
    {
            _emailSender = emailSender;
        _logger = logger;
        _unitOfwork = unitOfWork;
    }

    public IActionResult Index()
    {
        var clamidentity = (ClaimsIdentity)User.Identity;
        var userId = clamidentity.FindFirst(ClaimTypes.NameIdentifier).Value;    

        ShoppingCartVM shoppingCartVM = new (){
            ShoppingCartList = _unitOfwork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties:"Product"),
            OrderHeader=new()
        } ;

        foreach(var cart in shoppingCartVM.ShoppingCartList)
        {
            cart.Price = GetPriceBasedOnQuantity(cart);
            shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
        }

        return View(shoppingCartVM);
    }
    public IActionResult Summary()
    {
        var clamidentity = (ClaimsIdentity)User.Identity;
        var userId = clamidentity.FindFirst(ClaimTypes.NameIdentifier).Value;    

        ShoppingCartVM shoppingCartVM = new (){
            ShoppingCartList = _unitOfwork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties:"Product"),
            OrderHeader=new()
        } ;
        
        var user = _unitOfwork.AppUser.Get(u => u.Id == userId,includeProperties:"Address");
        shoppingCartVM.OrderHeader.ApplicationUser = user ;// _unitOfwork.AppUser.Get(u => u.Id == userId,includeProperties:"Address");
        shoppingCartVM.OrderHeader.Name = user.DisplayName;
        shoppingCartVM.OrderHeader.PhoneNumber = user.PhoneNumber;
        if(user.Address !=  null)
        {
            shoppingCartVM.OrderHeader.StreetAddress = shoppingCartVM.OrderHeader.ApplicationUser.Address.Street;
            shoppingCartVM.OrderHeader.City = shoppingCartVM.OrderHeader.ApplicationUser.Address.City;
            shoppingCartVM.OrderHeader.State = shoppingCartVM.OrderHeader.ApplicationUser.Address.State;
            shoppingCartVM.OrderHeader.PostalCode = shoppingCartVM.OrderHeader.ApplicationUser.Address.ZipCode;
        
        }
        

        foreach(var cart in shoppingCartVM.ShoppingCartList)
        {
            cart.Price = GetPriceBasedOnQuantity(cart);
            shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
        }

        return View(shoppingCartVM);
    }

    [HttpPost]
    [ActionName("Summary")]
    public IActionResult SummaryPOST()
    {
        var clamidentity = (ClaimsIdentity)User.Identity;
        var userId = clamidentity.FindFirst(ClaimTypes.NameIdentifier).Value;    

        ShoppingCartVM.ShoppingCartList = _unitOfwork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties:"Product");
        
        ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.UtcNow;
        ShoppingCartVM.OrderHeader.ApplicationUserId = userId;
        
        AppUser appuser  = _unitOfwork.AppUser.Get(u => u.Id == userId,includeProperties:"Address");
        

        foreach(var cart in ShoppingCartVM.ShoppingCartList)
        {
            cart.Price = GetPriceBasedOnQuantity(cart);
            ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
        }

        if(appuser.CompanyId.GetValueOrDefault() == 0)
        {
            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
        }
        else
        {
            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
        }

        if(ModelState.IsValid)
        {
            _unitOfwork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfwork.Save();

        }

        
        foreach(var cart in ShoppingCartVM.ShoppingCartList)
        {
            OrderDetail orderDetail = new(){
                ProductId = cart.ProductId,
                OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                Price = cart.Price,
                Count = cart.Count
            };
            _unitOfwork.OrderDetail.Add(orderDetail);
            _unitOfwork.Save();
        }

        if(appuser.CompanyId.GetValueOrDefault() == 0)
        {
            var domain = "http://localhost:5282/";
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}", //"https://example.com/success",
                CancelUrl = domain + $"customer/cart/index",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach(var itme in ShoppingCartVM.ShoppingCartList){
                var sessionLineItem = new Stripe.Checkout.SessionLineItemOptions{
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions{
                        UnitAmount = (long)(itme.Price * 100),
                        Currency = "usd",
                        ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions{
                            Name = itme.Product.Title
                        }
                    },
                    Quantity = itme.Count
                };
                options.LineItems.Add(sessionLineItem);
            }
            
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);
            
            _unitOfwork.OrderHeader.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id
                , session.Id, session.PaymentIntentId);
            
            _unitOfwork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }

        return RedirectToAction(nameof(OrderConfirmation), new {id = ShoppingCartVM.OrderHeader.Id});
    }

    public IActionResult OrderConfirmation(int Id)
    {
        OrderHeader orderHeader = _unitOfwork .OrderHeader.Get(u => u.Id == Id,includeProperties:"ApplicationUser");
        if(orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
        {
            //this is an ordeer by customer

            var service  = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Get(orderHeader.SessionId);
            if(session.PaymentStatus.ToLower() == "paid"){
                _unitOfwork.OrderHeader.UpdateStripePaymentId(Id, session.Id, session.PaymentIntentId);
                _unitOfwork.OrderHeader.UpdateStatus(Id  ,SD.StatusApproved, SD.PaymentStatusApproved);
                _unitOfwork.Save();
            }

            HttpContext.Session.Clear();
        }

        _emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "New Order ",$" New Order Created and the number is - {orderHeader.Id}");

        List<ShoppingCart> shoppingCarts = _unitOfwork.ShoppingCart.GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

        _unitOfwork.ShoppingCart.RemoveRange(shoppingCarts);
        _unitOfwork.Save();

        return View(Id);
    }
    public IActionResult Plus(int cartId)
    {
        var cartFormDb = _unitOfwork.ShoppingCart.Get(u => u.Id == cartId);
        cartFormDb.Count += 1;
        _unitOfwork.ShoppingCart.Update(cartFormDb);
        _unitOfwork.Save();
        return  RedirectToAction(nameof(Index));

    }
    public IActionResult Minus(int cartId)
    {
        var cartFormDb = _unitOfwork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
        if(cartFormDb.Count <= 1)
        {
            //Session set
            HttpContext.Session.SetInt32(SD.SessionCart, 
            _unitOfwork.ShoppingCart.GetAll(u=>u.ApplicationUserId == cartFormDb.ApplicationUserId).Count() - 1);
            
            _unitOfwork.ShoppingCart.Remove(cartFormDb);
        }
        else
        {
            cartFormDb.Count -= 1;
            _unitOfwork.ShoppingCart.Update(cartFormDb);
        }
        _unitOfwork.Save();
        return  RedirectToAction(nameof(Index));
    }

    public IActionResult Remove(int cartId)
    {
        var cartFormDb = _unitOfwork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
         //Session set
        HttpContext.Session.SetInt32(SD.SessionCart, 
            _unitOfwork.ShoppingCart.GetAll(u=>u.ApplicationUserId == cartFormDb.ApplicationUserId).Count() - 1);
        
        _unitOfwork.ShoppingCart.Remove(cartFormDb);
        _unitOfwork.Save();
        
        return  RedirectToAction(nameof(Index));

    }

    private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
    {
        if(shoppingCart.Count <= 50)
        {
            return shoppingCart.Product.Price;
        }
        else
        {
            if(shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;
            }
            else    
            {
                return shoppingCart.Product.Price100;
            }
        }
    }
}
