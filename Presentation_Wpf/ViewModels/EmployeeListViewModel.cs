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

public partial class EmployeeListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEmployeeService _employeeService;

    [ObservableProperty]
    private ObservableCollection<Employee> _employees = [];

    [ObservableProperty]
    private EmployeeRegistrationForm _employeeForm = new();

    [ObservableProperty]
    private ProjectRegistrationForm _projectForm = new();

    [ObservableProperty]
    private Employee _employee = new();

    [ObservableProperty]
    private string? _message;

    [ObservableProperty]
    private string? _deleteMessage;

    public EmployeeListViewModel(IServiceProvider serviceProvider, IEmployeeService employeeService)
    {
        _serviceProvider = serviceProvider;
        _employeeService = employeeService;
        GetEmployees();
    }

    [RelayCommand]
    public void GoToProjectAddView()
    {
        var projectAddViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
        projectAddViewModel.Form = ProjectForm;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectAddViewModel;
    }

    public void GoToProjectAddView(Project project)
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
        if (result.Success)
        {
            GetEmployees();
        }
        Message = result.ErrorMessage!;
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
    public void AddToNewProject(Employee employee)
    {
        var projectAddViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
        projectAddViewModel.Form = ProjectForm;
        projectAddViewModel.Form.ProjectManager.Id = employee.Id;
        projectAddViewModel.Form.ProjectManager.EmploymentNumber = employee.EmployementNumber;
        projectAddViewModel.Form.ProjectManager.FirstName = employee.FirstName;
        projectAddViewModel.Form.ProjectManager.LastName = employee.LastName;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectAddViewModel;
    }

    [RelayCommand]
    public async Task Delete(Employee employee)
    {
        var result = await _employeeService.DeleteEmployeeAsync(x => x.Id == employee.Id);
        if (result.Success)
        {
            GetEmployees();
        }
        DeleteMessage = result.ErrorMessage!;
    }

    public async void GetEmployees()
    {
        Employees = new ObservableCollection<Employee>(await _employeeService.GetAllEmployeesAsync());
    }
}
