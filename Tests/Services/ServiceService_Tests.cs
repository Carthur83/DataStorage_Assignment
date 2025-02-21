using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Tests.Contexts;
using Tests.Helpers;

namespace Tests.Services;

public class ServiceService_Tests
{
    private readonly DataContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceService _serviceService;

    public ServiceService_Tests()
    {
        _context = DataContextFactory_Test.Create();
        _serviceProvider = TestServiceProvider.Create(_context);
        _serviceService = _serviceProvider.GetRequiredService<IServiceService>();
    }

    [Fact]
    public async Task CreateAsync_ShouldAddServiceToDatabase_AndReturnIResult()
    {
        // Arrange
        var form = new ServiceRegistrationForm { ServiceName = "Test", Price = 1000 };

        // Act
        var result = await _serviceService.CreateServiceAsync(form);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetAllServicesAsync_ShouldReturnListOfAllCustomers()
    {
        // Arrange
        var form = new ServiceRegistrationForm { ServiceName = "Test", Price = 1000 };
        await _serviceService.CreateServiceAsync(form);

        // Act
        var result = await _serviceService.GetAllServicesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(form.ServiceName, result.FirstOrDefault(x => x.ServiceName == form.ServiceName)!.ServiceName);
    }

    [Fact]
    public async Task GetCustomerAsync_ShouldReturnServices()
    {
        // Arrange
        var form = new ServiceRegistrationForm { ServiceName = "Test", Price = 1000 };
        await _serviceService.CreateServiceAsync(form);


        // Act
        var result = await _serviceService.GetServiceAsync(x => x.ServiceName == form.ServiceName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.ServiceName);
    }

    [Fact]
    public async Task UpdateServiceAsync_ShouldUpdate_AndReturnUpdatedIResult()
    {
        // Arrange
        var form = new ServiceRegistrationForm { ServiceName = "Test", Price = 1000 };
        await _serviceService.CreateServiceAsync(form);

        var service = await _serviceService.GetServiceAsync(x => x.ServiceName == form.ServiceName);
        var updated = ServiceFactory.Create(service);
        

        // Act
        var result = await _serviceService.UpdateServiceAsync(x => x.Id == service.Id, updated);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task DeleteService_ShouldDeleteServiceFromDatabase_AndReturnIResult()
    {
        // Arrange
        var form = new ServiceRegistrationForm { ServiceName = "Test", Price = 1000 };
        await _serviceService.CreateServiceAsync(form);
        var service = await _serviceService.GetServiceAsync(x => x.ServiceName == form.ServiceName);

        // Act
        var result = await _serviceService.DeleteServiceAsync(x => x.Id == service.Id);

        // Assert
        Assert.True(result.Success);
        Assert.Empty(await _serviceService.GetAllServicesAsync());
    }

    [Fact]
    public async Task CheckIfExists_ShouldReturnTrue_IfCustomerAlreadyExists()
    {
        // Arrange
        var form = new ServiceRegistrationForm { ServiceName = "Test", Price = 1000 };
        await _serviceService.CreateServiceAsync(form);
        var service = await _serviceService.GetServiceAsync(x => x.ServiceName == form.ServiceName);

        // Act
        var result = await _serviceService.CheckIfExistsAsync(x => x.ServiceName == service.ServiceName);

        // Assert
        Assert.True(result);
    }
}
