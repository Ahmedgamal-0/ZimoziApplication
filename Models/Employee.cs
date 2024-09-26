using System.ComponentModel.DataAnnotations;

namespace ZimoziApplication.Models
{
    public class Employee
    {
        [Key] // Primary Key
        public int EmployeeID { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Department { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be positive.")]
        public decimal Salary { get; set; }
    }
}
