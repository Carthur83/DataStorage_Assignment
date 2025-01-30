using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation_Wpf.ViewModels;
using Presentation_Wpf.Views;
using System.Windows;

namespace Presentation_Wpf;

public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(servies =>
            {
                servies.AddDbContext<DataContext>(x => x.UseSqlServer(@""));

                servies.AddScoped<IProjectRepository, ProjectRepository>();
                servies.AddScoped<ICustomerRepository, CustomerRepository>();
                servies.AddScoped<IEmployeeRepository, EmployeeRepository>();
                servies.AddScoped<IServiceRepository, ServiceRepository>();
                servies.AddScoped<IStatusRepository, StatusRepository>();

                servies.AddScoped<ICustomerService, CustomerService>();
                servies.AddScoped<IProjectService, ProjectService>();
                servies.AddScoped<IEmployeeService, EmployeeService>();
                servies.AddScoped<IServiceService, ServiceService>();
                
                servies.AddScoped<IProjectFactory, ProjectFactory>();

                servies.AddSingleton<MainViewModel>();
                servies.AddSingleton<MainWindow>();

                servies.AddTransient<ProjectAddViewModel>();
                servies.AddTransient<ProjectAddView>();

            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
