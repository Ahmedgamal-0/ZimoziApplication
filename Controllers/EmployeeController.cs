using Microsoft.AspNetCore.Mvc;
using ZimoziApplication.Models;
using ZimoziApplication.Services;

namespace ZimoziApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> InsertEmployee(InsertEmployeeDto employee)
        {
            await _employeeService.InsertEmployee(employee);
            return Ok("Added Sucessfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee( Employee employee)
        {
            var existingEmployee = await _employeeService.UpdateEmployee(employee);
            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(existingEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployee(id);
            return NoContent(); 
        }
    }
}
