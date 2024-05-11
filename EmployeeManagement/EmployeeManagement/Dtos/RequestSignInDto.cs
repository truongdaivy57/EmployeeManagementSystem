using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Dtos
{
    public class RequestSignInDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
