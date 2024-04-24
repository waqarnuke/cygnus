using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly AppIdentityDbContext _context;
        public BrandRepository(AppIdentityDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Brand obj)
        {
            _context.Brands.Update(obj);
        }
    }
}