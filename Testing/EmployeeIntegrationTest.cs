using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ZimoziApplication.Testing
{
    public class EmployeeIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmployeeIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetEmployees_ReturnsSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/api/Employees");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("Ahmed Gamal");
        }

        [Fact]
        public async Task PostEmployee_CreatesEmployeeSuccessfully()
        {
            // Arrange
            var employee = new
            {
                Name = "Ahmed Gamal",
                Department = "HR",
                Salary = 5000
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Employees", employee);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdEmployee = await response.Content.ReadAsStringAsync();
            createdEmployee.Should().Contain("Ahmed Gamal");
        }
    }
}
