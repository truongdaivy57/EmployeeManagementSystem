using EmployeeManagement.Data;
using EmployeeManagement.Model;
using EmployeeManagement.Repository;

namespace EmployeeManagement.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        //IRepository<Order> OrderRepository { get; }
        //IRepository<Product> ProductRepository { get; }

        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<User> _userRepository;
        //private IRepository<Order> orderRepository;
        //private IRepository<Product> productRepository;

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

        //public IRepository<Order> OrderRepository
        //{
        //    get
        //    {
        //        if (orderRepository == null)
        //        {
        //            orderRepository = new OrderRepository(context);
        //        }

        //        return orderRepository;
        //    }
        //}

        //public IRepository<Product> ProductRepository
        //{
        //    get
        //    {
        //        if (productRepository == null)
        //        {
        //            productRepository = new ProductRepository(context);
        //        }

        //        return productRepository;
        //    }
        //}

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
