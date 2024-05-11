using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model
{
    [Table("Salary")]
    public class Salary
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseSalary { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSalary { get; set; }
        public DateTime CalculationDate { get; set; }
        public int OffDay { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
