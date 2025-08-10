using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppIdentityDbContext _context;

        public ProductRepository(AppIdentityDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product obj)
        {
            var objFormDb = _context.Products.FirstOrDefault(p => p.Id == obj.Id);
            if(objFormDb != null)
            {
                objFormDb.Title = obj.Title;
                objFormDb.ISBN = obj.ISBN;
                objFormDb.Price = obj.Price;
                objFormDb.Price50 = obj.Price50;
                objFormDb.Price100 = obj.Price100;
                objFormDb.ListPrice = obj.ListPrice;
                objFormDb.Description = obj.Description;
                objFormDb.CategoryId = obj.CategoryId;
                objFormDb.Author = obj.Author;
                if(obj.ImageUrl != null)
                {
                    objFormDb.ImageUrl = obj.ImageUrl;
                }
            }
             //_context.Products.Update(obj);
        }
    }
}