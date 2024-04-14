using Models;
using Models.Identity;

namespace DataAccess.Repository.IRepository
{
    public interface IAppUserRepository : IRepositoryIdentity<AppUser>
    {
    }
}