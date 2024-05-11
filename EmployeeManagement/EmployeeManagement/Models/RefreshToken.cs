using EmployeeManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string? Token { get; set; }
        public DateTime Expire { get; set; }
        public bool IsActive { get; set; }
    }
}
