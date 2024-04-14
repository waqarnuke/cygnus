using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility.Common;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWrok;
        public CompanyController(IUnitOfWork unitOfWrok, IWebHostEnvironment webHostEnviroment)
        {
            _unitOfWrok = unitOfWrok;
        }
        public IActionResult Index()
        {
            var objlist = _unitOfWrok.Company.GetAll().ToList();
            return View(objlist);
        }
        
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if(id ==null || id == 0)
            {
                //create 
                return View(new Company());
            }
            else
            {
                //udpate
                Company compnayObj = _unitOfWrok.Company.Get(u=>u.Id == id);
                return View(compnayObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company compnayObj)
        {
            if(ModelState.IsValid)
            {
                if(compnayObj.Id == 0)
                {
                    _unitOfWrok.Company.Add(compnayObj);
                }
                else
                {
                    _unitOfWrok.Company.Update(compnayObj);
                }
                _unitOfWrok.Save();
                TempData["success"] = "Compnay created successfully";
                return RedirectToAction("Index");
            }
            else{
                
                return View(compnayObj);
            }
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> listOfCompany = _unitOfWrok.Company.GetAll().ToList();
            return Json(new {data = listOfCompany});    
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companytoDelete = _unitOfWrok.Company.Get(p => p.Id == id);
            if(companytoDelete == null)
            {
                return Json(new {success=false,message="Error while deleting"});
            }
            
            _unitOfWrok.Company.Remove(companytoDelete);
            _unitOfWrok.Save();

            return Json(new { success=true, message="Delete Successful"});
        }

        #endregion API CALLS
    }
}