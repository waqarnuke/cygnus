using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility.Common;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IUnitOfWork _unitOfWrok;
        public CategoryController(IUnitOfWork unitOfWrok,IWebHostEnvironment webHostEnviroment,IConfiguration configuration)
        {
            _unitOfWrok = unitOfWrok;
            _webHostEnviroment = webHostEnviroment;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var objlist = _unitOfWrok.category.GetAll().ToList();
            return View(objlist);
        }
        
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            string productPath = _configuration.GetSection("Path").GetSection("ImageDefauldPath").Value;
            Category cat = new Category();
            if(id == null || id == 0)
            {
                cat.ImageUrl =  productPath ;
                return View(cat);
            }
            else
            {
                cat = _unitOfWrok.category.Get(u => u.Id == id);
                return View(cat);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(Category category, IFormFile? file)
        {
            string wwwRootPath = _webHostEnviroment.WebRootPath;
            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string categoryPath = Path.Combine(wwwRootPath,_configuration.GetSection("Path").GetSection("ImageProductPath").Value);

                    if(!string.IsNullOrEmpty(category.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(wwwRootPath, category.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath); 
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(categoryPath,fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    category.ImageUrl = "\\" + Path.Combine( _configuration.GetSection("Path").GetSection("ImageProductPath").Value,fileName);

                }
            
                if(category.Name == category.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name","The DisplayOrder cannot exactly match the Name."); 
                }
            
                if(category.Id == 0)
                {
                    _unitOfWrok.category.Add(category);
                }
                else
                {
                    _unitOfWrok.category.Update(category);
                }
                _unitOfWrok.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWrok.category.Get(u => u.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWrok.category.Get(u => u.Id == id);
            if(obj == null)
            {
                return  NotFound();
            }
            _unitOfWrok.category.Remove(obj);
            _unitOfWrok.Save();
            TempData["success"] = "Category delete successfully";
            return RedirectToAction("Index");
        }

    }
}