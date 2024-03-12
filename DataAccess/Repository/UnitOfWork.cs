using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository category {get; private set;}

        public IProductRepository Product {get; private set;}

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}