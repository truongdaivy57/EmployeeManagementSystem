using EmployeeManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    [Table("UserClaim")]
    public class UserClaim
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
