using System.Linq.Expressions;

namespace EmployeeManagement.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(Guid id);
        T Get(Guid id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
