
using Models;

namespace DataAccess.Repository.IRepository
{
    public interface ISiteConfigRepository : IRepository<SiteConfig>
    {
        void Update(SiteConfig obj);
        SiteConfig GetSiteConfig(string key);
    }
}