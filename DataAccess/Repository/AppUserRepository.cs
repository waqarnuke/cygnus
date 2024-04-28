using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.Identity;

namespace DataAccess.Repository
{
    public class AppUserRepository : RepositoryIdentity<AppUser>, IAppUserRepository
    {
        private readonly AppIdentityDbContext _context;
        public AppUserRepository(AppIdentityDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(AppUser appUser)
        {
            _context.AppUsers.Update(appUser);
        }
    }
}