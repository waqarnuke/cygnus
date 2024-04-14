using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        void Save();
    }
}