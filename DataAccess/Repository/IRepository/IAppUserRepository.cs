using Models.Identity;

namespace DataAccess.Repository.IRepository
{
    public interface IAppUserRepository : IRepositoryIdentity<AppUser>
    {
         public void Update(AppUser appUser);
    }
}