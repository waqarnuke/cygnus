using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class SiteConfigRepository : Repository<SiteConfig>, ISiteConfigRepository
    {
        private readonly AppIdentityDbContext _context;
        public SiteConfigRepository(AppIdentityDbContext context) : base(context)
        {
            _context = context;
        }

        public SiteConfig GetSiteConfig(string key)
        {
            return _context.SiteConfig.Find(key);
        }

        public void Update(SiteConfig obj)
        {
            _context.SiteConfig.Update(obj);
        }
    }
}