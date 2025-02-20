using Business.Dtos;
using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Tests.Contexts;
using Tests.Helpers;

namespace Tests.Services;

public class EmployeeService_Tests
{
    private readonly DataContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEmployeeService _employeeService ;

    public EmployeeService_Tests()
    {
        _context = DataContextFactory_Test.Create();
        _serviceProvider = TestServiceProvider.Create(_context);
        _employeeService = _serviceProvider.GetRequiredService<IEmployeeService>();
    }

    [Fact]
    public async Task CreateEmployeeAsync_ShouldAddEmployeeToDatabase_AndReturnIResult()
    {
        // Arrange
        var form = new EmployeeRegistrationForm { EmploymentNumber = "1162", FirstName = "Test", LastName = "Testing" };

        // Act
        var result = await _employeeService.CreateEmployeeAsync(form);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_ShouldReturnListOfAllEmployees()
    {
        // Arrange
        var form = new EmployeeRegistrationForm { EmploymentNumber = "1162", FirstName = "Test", LastName = "Testing" };
        await _employeeService.CreateEmployeeAsync(form);

        // Act
        var result = await _employeeService.GetAllEmployeesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.FirstOrDefault(x => x.FirstName == form.FirstName)!.FirstName);
    }

    [Fact]
    public async Task GetEmployeeAsync_ShouldReturnEmployee()
    {
        // Arrange
        var form = new EmployeeRegistrationForm { EmploymentNumber = "1162", FirstName = "Test", LastName = "Testing" };
        await _employeeService.CreateEmployeeAsync(form);

        // Act
        var result = await _employeeService.GetEmployeeAsync(x => x.EmploymentNumber == form.EmploymentNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Testing", result.LastName);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ShouldUpdate_AndReturnUpdatedIResult()
    {
        // Arrange
        var form = new EmployeeRegistrationForm { EmploymentNumber = "1162", FirstName = "Test", LastName = "Testing" };
        await _employeeService.CreateEmployeeAsync(form);

        var updated = await _employeeService.GetEmployeeAsync(x => x.EmploymentNumber == form.EmploymentNumber);
        updated.FirstName = "TestUpdated";

        // Act
        var result = await _employeeService.UpdateEmployeeAsync(x => x.Id == updated.Id, updated);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task DeleteEmployee_ShouldDeleteEmployeeFromDatabase_AndReturnIResult()
    {
        // Arrange
        var form = new EmployeeRegistrationForm { EmploymentNumber = "1162", FirstName = "Test", LastName = "Testing" };
        await _employeeService.CreateEmployeeAsync(form);
        var employee = await _employeeService.GetEmployeeAsync(x => x.EmploymentNumber == form.EmploymentNumber);

        // Act
        var result = await _employeeService.DeleteEmployeeAsync(x => x.Id == employee.Id);

        // Assert
        Assert.True(result.Success);
        Assert.Empty(await _employeeService.GetAllEmployeesAsync());
    }

    [Fact]
    public async Task CheckIfExists_ShouldReturnTrue_IfEmployeeAlreadyExists()
    {
        // Arrange
        var form = new EmployeeRegistrationForm { EmploymentNumber = "1162", FirstName = "Test", LastName = "Testing" };
        await _employeeService.CreateEmployeeAsync(form);
        var employee = await _employeeService.GetEmployeeAsync(x => x.EmploymentNumber == form.EmploymentNumber);

        // Act
        var result = await _employeeService.CheckIfExistsAsync(x => x.EmploymentNumber == employee.EmployementNumber);

        // Assert
        Assert.True(result);
    }
}
