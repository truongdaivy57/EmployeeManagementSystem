using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model
{
    [Table("User")]
    public class User : IdentityUser<Guid>
    {
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string LastName { get; set; }
        [DefaultValue(false)]
        public bool IsActive { get; set; }
        public string? VerificationToken { get; set; }
        public string? ResetToken { get; set; }
        public DateTime ResetTokenExpire {  get; set; }
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
