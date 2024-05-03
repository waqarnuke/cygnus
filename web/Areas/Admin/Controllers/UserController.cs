using System.Data;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Identity;
using Utility.Common;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly AppIdentityDbContext _context;
        public UserController(AppIdentityDbContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
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

        [HttpDelete]
        public IActionResult LockUnLock([FromBody]string Id)
        {
            var objFormDb = _context.AppUsers.FirstOrDefault(u => u.Id == Id);
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
            return Json(new {success = false, message =  "Delete Successful."});     
        }


        #endregion API CALLS
    }
}