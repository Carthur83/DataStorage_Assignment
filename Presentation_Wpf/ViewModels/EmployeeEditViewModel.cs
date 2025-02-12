using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_Wpf.ViewModels;

public partial class EmployeeEditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEmployeeService _employeeService;

    [ObservableProperty]
    private ObservableCollection<Employee> _employees = [];

    [ObservableProperty]
    private EmployeeRegistrationForm _employeeForm = new();

    [ObservableProperty]
    private Employee _employee = new();

    [ObservableProperty]
    private Project _project = new();

    public EmployeeEditViewModel(IServiceProvider serviceProvider, IEmployeeService employeeService)
    {
        _serviceProvider = serviceProvider;
        _employeeService = employeeService;
        GetEmployees();
    }

    [RelayCommand]
    public void GoToProjectEditView(Project project)
    {
        var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();
        projectEditViewModel.Project = project;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectEditViewModel;
    }

    [RelayCommand]
    public async Task AddEmployee(EmployeeRegistrationForm employeeForm)
    {
        var result = await _employeeService.CreateEmployeeAsync(employeeForm);
        GetEmployees();
        EmployeeForm = new();
    }

    [RelayCommand]
    public void GoToUpdate(Employee employee)
    {
        EmployeeForm = EmployeeFactory.CreateEmployeeForm(employee);
    }

    [RelayCommand]
    public async Task UpdateEmployee(EmployeeRegistrationForm updatedEmployee)
    {
        await _employeeService.UpdateEmployeeAsync(x => x.Id == updatedEmployee.Id, EmployeeFactory.Create(updatedEmployee));
        EmployeeForm = new();
        GetEmployees();
    }

    [RelayCommand]
    public void AddToProject(Employee employee)
    {
        var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();

        projectEditViewModel.Project = Project;
        projectEditViewModel.Project.EmployeeId = employee.Id;
        projectEditViewModel.Project.EmployeeFirstName = employee.FirstName;
        projectEditViewModel.Project.EmployeeLastName = employee.LastName;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectEditViewModel;
    }

    [RelayCommand]
    public async Task Delete(Employee employee)
    {
        var result = await _employeeService.DeleteEmployeeAsync(x => x.Id == employee.Id);
        if (result)
        {
            GetEmployees();
        }
    }

    public async void GetEmployees()
    {
        Employees = new ObservableCollection<Employee>(await _employeeService.GetAllEmployeesAsync());
    }
}
