using EmployeeManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("UserToken")]
    public class UserToken
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiredDateAccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredDateRefreshToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Key { get; set; }
    }
}
