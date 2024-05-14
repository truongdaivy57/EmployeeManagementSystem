using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeManagement.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
