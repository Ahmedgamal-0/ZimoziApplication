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

        // Injecting DbContext in the controller
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // 1. Get All Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(employees);
        }

        // 2. Get Employee by ID
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

        // 3. Insert a new Employee
        [HttpPost]
        public async Task<ActionResult> InsertEmployee(Employee employee)
        {
            await _employeeService.InsertEmployee(employee);
            // Return a Created response with the route to the newly created employee
            return Ok("Added Sucessfully");
        }

        // 4. Update an existing Employee
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

        // 5. Delete an Employee
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployee(id);
            return NoContent(); // Return 204 No Content when deletion is successful
        }
    }
}
