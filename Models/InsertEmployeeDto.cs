using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ZimoziApplication.Models
{
    public class InsertEmployeeDto
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}
