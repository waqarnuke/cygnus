using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ShopViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShopViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(ShopVM shopvmObj)
        {
            var search =  !String.IsNullOrEmpty(shopvmObj.search) ? shopvmObj.search : null ;

            ShopVM shopVM = new ShopVM();
            
            shopVM.PageNo = shopvmObj.PageNo.HasValue ? shopvmObj.PageNo > 0 ? shopvmObj.PageNo.Value : 1 : 1;

            var totalRecords = _unitOfWork.Product.GetCount(search != null ? s => s.Title.Contains(search) : null);

            shopVM.Products = _unitOfWork.Product.GetAll(includeProperties:"Category");
            
            if(shopVM.Products != null)
            {
                if(shopvmObj.CategoryId.HasValue)
                {
                    shopVM.Products = shopVM.Products.Where(x => x.CategoryId == shopvmObj.CategoryId.Value).ToList();
                // shopVM.Products = _unitOfWork.Product.GetAllWithPageSize(shopVM.PageNo.Value, u => u.CategoryId == shopvmObj.CategoryId.Value ,includeProperties:"Category").ToList();
                }

                if(!string.IsNullOrEmpty( shopvmObj.search))
                {
                    shopVM.Products = shopVM.Products.Where(u => u.Title.Contains(shopvmObj.search)).ToList();
                    //shopVM.Products = _unitOfWork.Product.GetAllWithPageSize(shopVM.PageNo.Value, u => u.Title.Contains(shopvmObj.search),includeProperties:"Category").ToList();
                }
                if(shopvmObj.MinimubPrice.HasValue)
                {
                    shopVM.Products = shopVM.Products.Where(u => u.Price >= shopvmObj.MinimubPrice.Value).ToList();
                }
                if(shopvmObj.MaximumPrice.HasValue)
                {
                    shopVM.Products = shopVM.Products.Where(u => u.Price <= shopvmObj.MaximumPrice.Value).ToList();
                }
                if(shopvmObj.SortBy.HasValue)
                {
                    switch (shopvmObj.SortBy.Value)
                    {
                        case 2:
                            shopVM.Products = shopVM.Products.OrderByDescending(u => u.Id).ToList();
                            break;
                        case 3:
                            shopVM.Products = shopVM.Products.OrderBy(u => u.Price).ToList();
                            break;
                        case 4:
                            shopVM.Products = shopVM.Products.OrderByDescending(u => u.Price).ToList();
                            break;
                        default:
                            shopVM.Products = shopVM.Products.OrderByDescending(u => u.Id).ToList();
                            break;
                    }
                    //products = products.Where(u => u.Price <= shopvmObj.MaximumPrice.Value).ToList();
                }
            
            totalRecords = shopVM.Products.Count();
            shopVM.Products =  shopVM.Products.Skip( Convert.ToInt32((shopVM.PageNo - 1) * 5)).Take(5).ToList();

            shopVM.Pager = new Pager(totalRecords,shopVM.PageNo.Value,5);


            shopVM.SortBy = shopvmObj.SortBy;
           }
           
            return View(shopVM);
        }

    }
}