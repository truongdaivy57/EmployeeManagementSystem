using EmployeeManagement.Data;
using EmployeeManagement.Model;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public override User Add(User user)
        {
            return base.Add(user);
        }

        public override User Get(Guid id)
        {
            return base.Get(id);
        }

        public override IEnumerable<User> All()
        {
            return base.All();
        }

        public override User Update(User user)
        {
            return base.Update(user);
        }

        public override void Delete(Guid id)
        {
            base.Delete(id);
        }
    }
}
