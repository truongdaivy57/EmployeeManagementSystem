using EmployeeManagement.Data;
using EmployeeManagement.Model;

namespace EmployeeManagement.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {

        }

        public override Department Add(Department department)
        {
            return base.Add(department);
        }

        public override Department Get(Guid id)
        {
            return base.Get(id);
        }

        public override IEnumerable<Department> All()
        {
            return base.All();
        }

        public override Department Update(Department department)
        {
            return base.Update(department);
        }

        public override void Delete(Guid id)
        {
            base.Delete(id);
        }
    }
}
