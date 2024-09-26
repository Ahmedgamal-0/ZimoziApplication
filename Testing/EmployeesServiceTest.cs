using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ZimoziApplication.Models;
using ZimoziApplication.Services;
using Xunit;

namespace ZimoziApplication.Testing
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService _service;
        private readonly ApplicationDbContext _context;

        public EmployeeServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeTestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new EmployeeService(_context);
        }

        [Fact]
        public async Task GetEmployeesAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            _context.Employees.Add(new Employee { Name = "Ahmed Gamal", Department = "HR", Salary = 5000 });
            _context.Employees.Add(new Employee { Name = "Jane Doe", Department = "Finance", Salary = 6000 });
            await _context.SaveChangesAsync();

            // Act
            var employees = await _service.GetEmployeesAsync();

            // Assert
            employees.Should().HaveCount(2);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldAddEmployeeToDatabase()
        {
            // Arrange
            var employee = new InsertEmployeeDto { Name = "Ahmed Gamal", Department = "IT", Salary = 7000 };

            // Act
            await _service.InsertEmployee(employee);

            // Assert
            var addedEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Name == "Ahmed Gamal");
            addedEmployee.Should().NotBeNull();
            addedEmployee.Salary.Should().Be(7000);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldDeleteEmployeeFromDatabase()
        {
            // Arrange
            var employee = new Employee { Name = "Ahmed Gamal", Department = "IT", Salary = 7000 };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Act
            await _service.DeleteEmployee(employee.EmployeeID);

            // Assert
            var deletedEmployee = await _context.Employees.FindAsync(employee.EmployeeID);
            deletedEmployee.Should().BeNull();
        }
    }

}
