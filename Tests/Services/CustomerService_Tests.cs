using Business.Dtos;
using Business.Interfaces;
using Data.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Tests.Contexts;
using Tests.Helpers;

namespace Tests.Services;

public class CustomerService_Tests
{
    private readonly DataContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly ICustomerService _customerService;

    public CustomerService_Tests()
    {
        _context = DataContextFactory_Test.Create();
        _serviceProvider = TestServiceProvider.Create(_context);
        _customerService = _serviceProvider.GetRequiredService<ICustomerService>();

    }

    [Fact]
    public async Task CreateAsync_ShouldAddCustomerToDatabase_AndReturnIResult()
    {
        // Arrange
        var form = new CustomerRegistrationForm { CustomerName = "Company" };

        // Act
        var result = await _customerService.CreateCustomerAsync(form);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetAllCustomersAsync_ShouldReturnListOfAllCustomers()
    {
        // Arrange
        var customer = new CustomerRegistrationForm { CustomerName = "Testing" };
        await _customerService.CreateCustomerAsync(customer);

        // Act
        var result = await _customerService.GetAllCustomersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(customer.CustomerName, result.FirstOrDefault(x => x.CustomerName == customer.CustomerName)!.CustomerName);
    }

    [Fact]
    public async Task GetCustomerAsync_ShouldReturnCustomer()
    {
        // Arrange
        var customer = new CustomerRegistrationForm { CustomerName = "Testing" };
        await _customerService.CreateCustomerAsync(customer);

        // Act
        var result = await _customerService.GetCustomerAsync(x => x.CustomerName == customer.CustomerName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Testing", result.CustomerName);
    }

    [Fact]
    public async Task UpdateCustomerAsync_ShouldUpdate_AndReturnUpdatedIResult()
    {
        // Arrange
        var customer = new CustomerRegistrationForm { CustomerName = "Testing" };
        await _customerService.CreateCustomerAsync(customer);

        var updatedCustomer = await _customerService.GetCustomerAsync(x => x.CustomerName == customer.CustomerName);
        updatedCustomer.CustomerName = "TestingUpdated";

        // Act
        var result = await _customerService.UpdateCustomerAsync(x => x.Id == updatedCustomer.Id, updatedCustomer);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task DeleteCustomer_ShouldDeleteCustomerFromDatabase_AndReturnIResult()
    {
        // Arrange
        var form = new CustomerRegistrationForm { CustomerName = "Testing" };
        await _customerService.CreateCustomerAsync(form);
        var customer = await _customerService.GetCustomerAsync(x => x.CustomerName == form.CustomerName);

        // Act
        var result = await _customerService.DeleteCustomerAsync(x => x.Id == customer.Id);

        // Assert
        Assert.True(result.Success);
        Assert.Empty(await _customerService.GetAllCustomersAsync());
    }

    [Fact]
    public async Task CheckIfExists_ShouldReturnTrue_IfCustomerAlreadyExists()
    {
        // Arrange
        var form = new CustomerRegistrationForm { CustomerName = "Testing" };
        await _customerService.CreateCustomerAsync(form);
        var customer = await _customerService.GetCustomerAsync(x => x.CustomerName == form.CustomerName);

        // Act
        var result = await _customerService.CheckIfExistsAsync(x => x.CustomerName == customer.CustomerName);

        // Assert
        Assert.True(result);
    }
}
