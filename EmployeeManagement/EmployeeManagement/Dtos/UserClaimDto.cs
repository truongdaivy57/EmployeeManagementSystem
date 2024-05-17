namespace EmployeeManagement.Dtos
{
    public class UserClaimsDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<ClaimDto> Claims { get; set; }
    }
}
