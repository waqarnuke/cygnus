using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        private readonly AppIdentityDbContext _context;
        public SubCategoryRepository(AppIdentityDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(SubCategory obj)
        {
            _context.SubCategories.Update(obj);
        }
    }
}