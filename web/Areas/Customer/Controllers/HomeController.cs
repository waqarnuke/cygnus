using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;
using DataAccess.Repository.IRepository;
using Utility.Common;

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
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
