using System.Linq.Expressions;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T,bool>>? filter=null,string? includeProperties = null);
        T Get(Expression<Func<T,bool>> filter,string? includeProperties = null, bool tracked=false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        //Testing for pageer but never used if someone will use please remve this commints 
        IEnumerable<T> GetAllWithPager(Expression<Func<T,bool>>? filter=null, string? includeProperties = null, int? pageNo=null);
        //Testing for pageer but never used if someone will use please remve this commints 
        int GetCount(Expression<Func<T, bool>>? filter);
        IEnumerable<T> AddRange(List<T>? entity = null);
    }
}