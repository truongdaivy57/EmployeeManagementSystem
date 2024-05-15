using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Dtos
{
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
