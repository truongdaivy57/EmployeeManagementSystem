using System.Linq.Expressions;

namespace EmployeeManagement.Repositories
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Add(T entity);
        T Update(T entity);
        void Delete(Guid id);
        void SaveChanges();
        Task InsertAsync(T entity);
        Task SaveChangesAsync();
        Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
