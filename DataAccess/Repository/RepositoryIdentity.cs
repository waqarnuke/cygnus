using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace DataAccess.Repository
{
    public class RepositoryIdentity<T> : IRepositoryIdentity<T> where T : class
    {
        private readonly AppIdentityDbContext _context;
        private DbSet<T> _dbSet;
        public RepositoryIdentity(AppIdentityDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false )
        {
            IQueryable<T> query ;
            if(tracked)
            {
                query = _dbSet;   
            }
            else
            {
                query  = _dbSet.AsNoTracking();
            }
            query = query.Where(filter);
                if(!string.IsNullOrEmpty(includeProperties))
                {
                    foreach(var includporp in includeProperties
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includporp);
                    }
                }
                return query.FirstOrDefault();
            
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            
            }
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includporp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includporp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
           _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }
    }
}