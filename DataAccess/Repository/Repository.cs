using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //private readonly ApplicationDbContext _context;
        private readonly AppIdentityDbContext _context;
        private DbSet<T> _dbSet;
        public Repository(AppIdentityDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _context.Products.Include(c => c.Category).Include(c=>c.CategoryId);
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
        //Testing for pageer but never used if someone will use please remve this commints 
        public IEnumerable<T> GetAllWithPager(Expression<Func<T, bool>>? filter, string? includeProperties = null, int? pageNo=null)
        {
            int pagesize = 3;
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
            if(pageNo.HasValue)
            {
                query = query.Skip( Convert.ToInt32((pageNo - 1) * pagesize)).Take(pagesize);
            }
            return query.ToList();
        }
        //Testing for pageer but never used if someone will use please remve this commints 
        public int GetCount(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                return query.Where(filter).Count();
            
            }
            else
            {
                return  query.Count();
            }
            
        }
        public void Remove(T entity)
        {
           _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }
        public IEnumerable<T> AddRange( List<T>? entity = null)
        {
            

            _dbSet.AddRange(entity);

            return entity.ToList();
        }

        
    }
}