using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;

namespace Web.Areas.Admin.Controllers
{
     [Area("Admin")]
    public class ProductController : Controller
    {
         private readonly IUnitOfWork _unitOfWrok;
        public ProductController(IUnitOfWork unitOfWrok)
        {
            _unitOfWrok = unitOfWrok;
        }
        public IActionResult Index()
        {
            var objlist = _unitOfWrok.Product.GetAll().ToList();
            return View(objlist);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWrok.category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value=u.Id.ToString()
                });
            ViewBag.CategoryList = categoryList;
            ProductVM productVM = new ProductVM(){
                CategoryList = categoryList,
                Product = new Product()
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                _unitOfWrok.Product.Add(productVM.Product);
                _unitOfWrok.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            else{
                productVM.CategoryList = _unitOfWrok.category.GetAll().Select(u => new SelectListItem 
                {
                    Text = u.Name,
                    Value = u.Id.ToString()    
                });
                return View(productVM);
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWrok.Product.Get(u => u.Id == id);
            if(product == null)
            {
                return NotFound();
            } 
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                _unitOfWrok.Product.Update(product);
                _unitOfWrok.Save();
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public IActionResult Delete(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWrok.Product.Get(u => u.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWrok.Product.Get(u => u.Id == id);
            if(obj == null)
            {
                return  NotFound();
            }
            _unitOfWrok.Product.Remove(obj);
            _unitOfWrok.Save();
            TempData["success"] = "Category delete successfully";
            return RedirectToAction("Index");
        }
    }
}