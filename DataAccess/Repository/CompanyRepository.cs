using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Company obj)
        {
            _context.Company.Update(obj);
        }
    }
}