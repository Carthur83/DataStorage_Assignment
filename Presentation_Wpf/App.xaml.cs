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
                servies.AddScoped<IProjectServiceRepository, ProjectServiceRepository>();

                servies.AddScoped<ICustomerService, CustomerService>();
                servies.AddScoped<IProjectService, ProjectService>();
                servies.AddScoped<IEmployeeService, EmployeeService>();
                servies.AddScoped<IServiceService, ServiceService>();
                servies.AddScoped<IStatusService, StatusService>();
                servies.AddScoped<IProjectServiceService, ProjectServiceService>();

                servies.AddSingleton<MainViewModel>();
                servies.AddSingleton<MainWindow>();

                servies.AddTransient<ProjectAddViewModel>();
                servies.AddTransient<ProjectAddView>();
                servies.AddTransient<ProjectListViewModel>();
                servies.AddTransient<ProjectListView>();
                servies.AddTransient<ProjectEditViewModel>();
                servies.AddTransient<ProjectEditView>();

                servies.AddTransient<ServiceListViewModel>();
                servies.AddTransient<ServiceListView>();
                servies.AddTransient<ServiceEditViewModel>();
                servies.AddTransient<ServiceEditView>();

                servies.AddTransient<EmployeeListViewModel>();
                servies.AddTransient<EmployeeListView>();
                servies.AddTransient<EmployeeEditViewModel>();
                servies.AddTransient<EmployeeEditView>();

                servies.AddTransient<CustomerListViewModel>();
                servies.AddTransient<CustomerListView>();
                servies.AddTransient<CustomerEditViewModel>();
                servies.AddTransient<CustomerEditView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
