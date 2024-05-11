using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model
{
    [Table("Role")]
    public class Role : IdentityRole<Guid>
    {
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
    }
}
