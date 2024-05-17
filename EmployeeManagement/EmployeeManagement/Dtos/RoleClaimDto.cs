namespace EmployeeManagement.Dtos
{
    public class RoleClaimsDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ClaimDto> Claims { get; set; }
    }
}
