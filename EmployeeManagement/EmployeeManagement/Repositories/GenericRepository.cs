using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual T Add(T entity)
        {
            return _context
                .Add(entity)
                .Entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual T Get(Guid id)
        {
            return _context.Find<T>(id);
        }

        public virtual IEnumerable<T> All()
        {
            return _context.Set<T>()
                .AsQueryable()
                .ToList();
        }

        public virtual T Update(T entity)
        {
            return _context.Update(entity)
                .Entity;
        }

        public virtual void Delete(Guid id)
        {
            var entity = _context.Find<T>(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }
    }
}
