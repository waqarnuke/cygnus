using System.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using Utility.Common;
using OfficeOpenXml;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IUnitOfWork _unitOfWrok;
        public ProductController(IUnitOfWork unitOfWrok, IWebHostEnvironment webHostEnviroment)
        {
            _unitOfWrok = unitOfWrok;
            _webHostEnviroment = webHostEnviroment;
        }
        public IActionResult Index()
        {
            var objlist = _unitOfWrok.Product.GetAll(includeProperties:"Category").ToList();
            return View(objlist);
        }
        
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            string wwwRootPath = _webHostEnviroment.WebRootPath;
            string productPath = Path.Combine(wwwRootPath, @"\images\");
            ProductVM productVM = new ProductVM(){
                CategoryList = _unitOfWrok.category.GetAll().Select(u => new SelectListItem {
                    Text = u.Name,
                    Value=u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id ==null || id == 0)
            {
                //create 
                productVM.Product.ImageUrl = @"\images\placeholder.JPG" ;
                return View(productVM);
            }
            else
            {
                //udpate
                productVM.Product = _unitOfWrok.Product.Get(u=>u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath); 
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(productPath,fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl =  @"\images\product\" + fileName;

                }
                if(productVM.Product.Id == 0)
                {
                    _unitOfWrok.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWrok.Product.Update(productVM.Product);
                }
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
        
         [HttpGet]
        public IActionResult uploadFile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult uploadFile(ProductVM productVM, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"productFile");

                    
                    using(var fileStream = new FileStream(Path.Combine(productPath,fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    //Path.Combine(productPath,fileName
                    string csvFilePath = Path.Combine(wwwRootPath, @"productFile\") + fileName;
                    try
                    {
                        List<Product> products = new List<Product>();
                        //Create a table 
                        using(ExcelPackage package = new ExcelPackage(new FileInfo(csvFilePath)))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Assuming the first worksheet

                            int rowCount = worksheet.Dimension.End.Row;
                            int colCount = worksheet.Dimension.End.Column;
                            for (int row = 2; row <= rowCount; row++)
                            {
                                Product product = new Product();
                                for (int col = 1; col <= colCount; col++)
                                {
                                    string columnHeader = worksheet.Cells[1, col].Text;
                                    string cellValue = worksheet.Cells[row, col].Text;
                                    // Map column header to product property
                                    switch (columnHeader)
                                    {
                                        // case "Id":
                                        //     product.Id = Convert.ToInt32(cellValue) ;
                                        //     break;
                                        case "Title":
                                            product.Title = cellValue;
                                            break;
                                        case "Description":
                                            product.Description = cellValue;
                                            break;
                                        case "ISBN":
                                            product.ISBN = cellValue;
                                            break;
                                        case "Author":
                                            product.Author = cellValue;
                                            break;  
                                        case "CategoryId":
                                            product.CategoryId = Convert.ToInt32(cellValue);
                                            break;
                                        case "ImageUrl":
                                            product.ImageUrl = cellValue;
                                            break;
                                        case "ListPrice":
                                            product.ListPrice = Convert.ToDouble(cellValue);
                                            break;  
                                        case "Price":
                                            product.Price = Convert.ToDouble(cellValue);
                                            break;
                                        case "Price50":
                                            product.Price50 = Convert.ToDouble(cellValue);
                                            break;
                                        case "Price100":
                                           product.Price100 = Convert.ToDouble(cellValue);
                                            break;                    
                                        // Add more cases for additional properties
                                        default:
                                            // Handle unknown column header
                                            break;
                                    }
                                    
                                    
                                }

                            products.Add(product);
                            }
                        }
                        if(products.Count > 0)
                        {
                            var newlistAdded = _unitOfWrok.Product.AddRange(products);
                            _unitOfWrok.Save();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        
                        throw;
                    }
                    
                }
                // if(productVM.Product.Id == 0)
                // {
                //     _unitOfWrok.Product.Add(productVM.Product);
                // }
                // else
                // {
                //     _unitOfWrok.Product.Update(productVM.Product);
                // }
                // _unitOfWrok.Save();
                // TempData["success"] = "Category created successfully";
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
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> listOfProduct = _unitOfWrok.Product.GetAll(includeProperties:"Category").ToList();
            return Json(new {data = listOfProduct});    
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var producttoDelete = _unitOfWrok.Product.Get(p => p.Id == id);
            if(producttoDelete == null)
            {
                return Json(new {success=false,message="Error while deleting"});
            }
            if(!string.IsNullOrEmpty(producttoDelete.ImageUrl))
            {
                string oldImagePath = Path.Combine(_webHostEnviroment.WebRootPath, producttoDelete.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath); 
                        }
            }
            
            _unitOfWrok.Product.Remove(producttoDelete);
            _unitOfWrok.Save();

            return Json(new { success=true, message="Delete Successful"});
        }

        #endregion API CALLS
    }
}