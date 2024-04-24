namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository category{get;}
        IProductRepository Product{get;}
        ICompanyRepository Company{get;}
        IShoppingCartRepository ShoppingCart {get;}
        IAppUserRepository AppUser {get;}
        IOrderDetailRepository OrderDetail {get;}
        IOrderHeaderRepository OrderHeader {get;}
        IBrandRepository Brand {get;}
        ISubCategoryRepository SubCategory {get;}
        void Save();
    }
}