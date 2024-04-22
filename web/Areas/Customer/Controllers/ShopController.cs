using System.Security.Claims;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models;
using Utility.Common;
using Web.ViewModel;

namespace Web.Area.Customer.Controllers
{
    [Area("Customer")]
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ShopController(ILogger<ShopController> logger , IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index(string search=null)
        {
            var clamidentity = (ClaimsIdentity)User.Identity;
            var claim = clamidentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim != null )
            {
                HttpContext.Session.SetInt32(SD.SessionCart, 
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
            }

            return View(new ShopVM());
        }

        public IActionResult GetNewList(string search, int? pageNo, int? categoryId,int sortBy)
        {
            
            ShopVM obj = new ShopVM()
            {
                search =  search,
                PageNo = pageNo,
                CategoryId = categoryId,
                SortBy = sortBy
            };
            return ViewComponent("Shop", obj);
        }

       
        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new (){
                Product = _unitOfWork.Product.Get(p => p.Id == productId, includeProperties:"Category"),
                Count = 1,
                ProductId = productId
            };
            
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var clamidentity = (ClaimsIdentity)User.Identity;
            var userId = clamidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            cart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == cart.ProductId );

            if(cartFromDb != null){
                cartFromDb.Count += cart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            { //add cart record
                _unitOfWork.ShoppingCart.Add(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, 
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }

            TempData["success"] = "Cart updated successfully";
            
            return Redirect(nameof(Index));
        }

        public IActionResult shoplist(string search=null)
        {
            var clamidentity = (ClaimsIdentity)User.Identity;
            var claim = clamidentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim != null )
            {
                HttpContext.Session.SetInt32(SD.SessionCart, 
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
            }

            return View(new ShopVM());
        }
        
    }
}