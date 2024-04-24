using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataAccess.Repository.IRepository;
using Utility.Common;
using Models.ViewModels;
namespace Web.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var clamidentity = (ClaimsIdentity)User.Identity;
        if(clamidentity.Claims.Count() != 0)
        {
            var claim = clamidentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim.Value != null )
            {
                HttpContext.Session.SetInt32(SD.SessionCart, 
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
            }
        }
        
        HomeVM model = new()
        {
            Products = _unitOfWork.Product.GetAll(),
            Categories = _unitOfWork.category.GetAll(),
            Brands = _unitOfWork.Brand.GetAll()
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
