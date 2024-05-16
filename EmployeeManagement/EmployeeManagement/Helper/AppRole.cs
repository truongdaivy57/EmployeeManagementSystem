using System.Reflection;

namespace EmployeeManagement.Helper
{
    public class AppRole
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Employee = "Employee";

        public static string[] GetAllRoles()
        {
            return new string[] { Admin, Manager, Employee };
        }
    }
}
