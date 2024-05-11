namespace EmployeeManagement.Dtos
{
    public class ResponseSignInDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
