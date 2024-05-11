namespace EmployeeManagement.Model
{
    public class Salary
    {
        public int Id { get; set; }
        public double BaseSalary { get; set; }
        public int VacationDays { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
