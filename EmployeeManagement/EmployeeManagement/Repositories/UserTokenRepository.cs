using EmployeeManagement.Data;
using EmployeeManagement.Model;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public class UserTokenRepository : GenericRepository<UserToken>
    {
        public UserTokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}
