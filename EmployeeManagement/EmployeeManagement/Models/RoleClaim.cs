using EmployeeManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    [Table("RoleClaim")]
    public class RoleClaim
    {
        [Key]
        public Guid Id { get; set; }

        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
