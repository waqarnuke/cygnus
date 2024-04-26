using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;

namespace Web.Controllers
{
    public class CategoryViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HomeVM homevm = new ()
            {
                Categories = _unitOfWork.category.GetAll().ToList(),
                Brands = _unitOfWork.Brand.GetAll()
            };
            IEnumerable<SelectListItem> categorytList = _unitOfWork.category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            
            return View(homevm);
        }

    }
}