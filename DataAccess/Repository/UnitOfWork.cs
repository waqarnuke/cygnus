using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppIdentityDbContext _context;
         private readonly AppIdentityDbContext _contextIdentity;
        public ICategoryRepository category {get; private set;}

        public IProductRepository Product {get; private set;}
        public ICompanyRepository Company {get; private set;}
        public IShoppingCartRepository ShoppingCart {get; private set;} 
        public IAppUserRepository AppUser{get; private set;}
        public IOrderDetailRepository OrderDetail {get; private set;}
        public IOrderHeaderRepository OrderHeader {get; private set;}
        public UnitOfWork(AppIdentityDbContext context)
        {
            _context = context;
            
            category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            Company = new CompanyRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
            AppUser = new AppUserRepository(context);
            OrderDetail = new OrderDetailRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}