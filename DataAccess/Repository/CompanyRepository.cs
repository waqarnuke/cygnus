using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly AppIdentityDbContext _context;
        public CompanyRepository(AppIdentityDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Company obj)
        {
            _context.Company.Update(obj);
        }
    }
}