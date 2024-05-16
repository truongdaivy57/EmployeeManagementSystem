using EmployeeManagement.Data;
using EmployeeManagement.Model;
using EmployeeManagement.Models;
using EmployeeManagement.Repository;

namespace EmployeeManagement.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Department> DepartmentRepository { get; }
        void SaveChanges();
        Task CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<User> _userRepository;
        private IRepository<Department> _departmentRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public IRepository<Department> DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository(_context);
                }

                return _departmentRepository;
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
