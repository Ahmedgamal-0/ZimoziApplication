using Microsoft.EntityFrameworkCore;
using ZimoziApplication.Models;

namespace ZimoziApplication.Services
{
    public class EmployeeService:IEmployeeService
    {
        #region vars & props
        private readonly ApplicationDbContext _context;
        #endregion
        #region constructor
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;   
        }

        #endregion
        #region methods
        public async Task InsertEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeID);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            existingEmployee.Salary = employee.Salary;
            _context.Entry(existingEmployee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.EmployeeID == employee.EmployeeID))
                throw;
            }
            return existingEmployee;
        }
        public async Task DeleteEmployee(int employeeId)
        {
            var existingEmployee = await _context.Employees.FindAsync(employeeId);

            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            _context.Employees.Remove(existingEmployee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.EmployeeID == employeeId))
                throw;
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            return existingEmployee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }
        #endregion
    }
}
