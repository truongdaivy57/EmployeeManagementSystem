using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model
{
    [Table("Application")]
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
