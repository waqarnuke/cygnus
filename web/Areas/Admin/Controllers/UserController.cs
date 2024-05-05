using System.Data;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Identity;
using Models.ViewModels;
using Utility.Common;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserController(AppIdentityDbContext context, IWebHostEnvironment webHostEnviroment, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult RoleManagment(string userId)
        {
            string RoleId = _context.UserRoles.FirstOrDefault(u => u.UserId == userId).RoleId;
            RoleManagmentVM RoleVM = new RoleManagmentVM(){
                ApplicationUser = _context.AppUsers.Include(u => u.Compnay).FirstOrDefault(u => u.Id == userId),
                RoleList = _context.Roles.Select( i => new SelectListItem{
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _context.Company.Select(i => new SelectListItem{
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            RoleVM.ApplicationUser.Role = _context.Roles.FirstOrDefault(u => u.Id == RoleId).Name;

            return View(RoleVM);
        }
        [HttpPost]
        public IActionResult RoleManagment(RoleManagmentVM roleManagmentVM)
        {
            string RoleId = _context.UserRoles.FirstOrDefault(u => u.UserId == roleManagmentVM.ApplicationUser.Id).RoleId;
            string oldRole = _context.Roles.FirstOrDefault(u => u.Id == RoleId).Name;
            
            if(!(roleManagmentVM.ApplicationUser.Role == oldRole))
            {
                AppUser appUser = _context.AppUsers.FirstOrDefault(u => u.Id == roleManagmentVM.ApplicationUser.Id);
                if(roleManagmentVM.ApplicationUser.Role == SD.Role_Company)
                {
                    appUser.CompanyId = roleManagmentVM.ApplicationUser.CompanyId;
                }
                if(oldRole == SD.Role_Company)
                {
                    appUser.CompanyId = null;
                }
                _context.SaveChanges();

                _userManager.RemoveFromRoleAsync(appUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(appUser, roleManagmentVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
        
        #region API CALLS 

        [HttpGet]
        public IActionResult GetAll()
        {
            List<AppUser> objUserList = _context.AppUsers.Include( u => u.Compnay).ToList();
            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();

            foreach (var item in objUserList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == item.Id).RoleId;
                item.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
                
                if(item.Compnay == null)
                {
                    item.Compnay = new Company(){Name = ""};
                }
            }
            return Json(new {data = objUserList});    
        }

        [HttpPost]
        public IActionResult LockUnLock([FromBody]string id)
        {
            var objFormDb = _context.AppUsers.FirstOrDefault(u => u.Id == id);
            if(objFormDb == null)
            {
                return Json(new {success = false, message =  "Error while Locking/Unlocking"}); 
            }
            if(objFormDb.LockoutEnd != null && objFormDb.LockoutEnd > DateTime.Now)
            {
                objFormDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFormDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _context.SaveChanges();
            return Json(new {success = true, message =  "Operation Successful."});     
        }


        #endregion API CALLS
    }
}