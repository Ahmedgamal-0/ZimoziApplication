using ZimoziApplication.Models;

namespace ZimoziApplication.Services
{
    public interface IEmployeeService
    {
        public Task InsertEmployee(InsertEmployeeDto employeeToInsert);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task DeleteEmployee(int employeeId);
        public Task<Employee> GetEmployeeById(int id);
        public Task<IEnumerable<Employee>> GetEmployeesAsync();

    }
}
