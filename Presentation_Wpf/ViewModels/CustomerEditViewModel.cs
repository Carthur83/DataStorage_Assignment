using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_Wpf.ViewModels;

public partial class CustomerEditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICustomerService _customerService;

    public CustomerEditViewModel(IServiceProvider serviceProvider, ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;
        GetCustomers();
    }

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

    [ObservableProperty]
    private CustomerRegistrationForm _customerForm = new();

    [ObservableProperty]
    private Customer _customer = new();

    [ObservableProperty]
    private Project _project = new();

    [ObservableProperty]
    private string? _message;

    [RelayCommand]
    public void GoToProjectEditView(Project project)
    {
        var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();
        projectEditViewModel.Project = project;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectEditViewModel;
    }

    [RelayCommand]
    public async Task AddCustomer(CustomerRegistrationForm form)
    {
        var result = await _customerService.CreateCustomerAsync(form);
        if (result.Success)
        {
            GetCustomers();
        }
        Message = result.ErrorMessage!;
    }

    [RelayCommand]
    public async Task Delete(Customer customer)
    {
        var result = await _customerService.DeleteCustomerAsync(x => x.Id == customer.Id);
        if (result.Success)
        {
            GetCustomers();
        }
    }

    [RelayCommand]
    public void AddToProject(Customer customer)
    {
        var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();
        projectEditViewModel.Project = Project;
        projectEditViewModel.Project.CustomerId = customer.Id;
        projectEditViewModel.Project.CustomerName = customer.CustomerName;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectEditViewModel;
    }

    [RelayCommand]
    public void GoToUpdate(Customer customer)
    {
        CustomerForm = CustomerFactory.CreateCustomerForm(customer);
    }

    [RelayCommand]
    public async Task UpdateCustomer(CustomerRegistrationForm updatedCustomer)
    {
        await _customerService.UpdateCustomerAsync(x => x.Id == updatedCustomer.Id, CustomerFactory.Create(updatedCustomer));
        GetCustomers();
    }

    public async void GetCustomers()
    {
        Customers = new ObservableCollection<Customer>(await _customerService.GetAllCustomersAsync());
    }
}
