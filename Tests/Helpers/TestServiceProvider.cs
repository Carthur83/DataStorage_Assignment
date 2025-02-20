using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Helpers;

public class TestServiceProvider
{
    public static ServiceProvider Create(DataContext context)
    {
        var services = new ServiceCollection();

        services.AddSingleton(context);

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IProjectServiceRepository, ProjectServiceRepository>();

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IProjectServiceService, ProjectServiceService>();
        services.AddScoped<IProjectService, ProjectService>();

        return services.BuildServiceProvider();
    }
}
