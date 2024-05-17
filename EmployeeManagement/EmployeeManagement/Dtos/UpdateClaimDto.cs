namespace EmployeeManagement.Dtos
{
    public class UpdateClaimDto
    {
        public string OldClaimType { get; set; }
        public string OldClaimValue { get; set; }
        public string NewClaimType { get; set; }
        public string NewClaimValue { get; set; }
    }
}
