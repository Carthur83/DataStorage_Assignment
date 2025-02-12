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

public partial class CustomerListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICustomerService _customerService;

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

    [ObservableProperty]
    private CustomerRegistrationForm _customerForm = new();

    [ObservableProperty]
    private Customer _customer = new();

    [ObservableProperty]
    private ProjectRegistrationForm _projectForm = new();

    public CustomerListViewModel(IServiceProvider serviceProvider, ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;
        GetCustomers();
    }

    [RelayCommand]
    public void GoToProjectAddView()
    {
        var projectAddViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
        projectAddViewModel.Form = ProjectForm;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectAddViewModel;
    }

    [RelayCommand]
    public async Task AddCustomer(CustomerRegistrationForm form)
    {
        var result = await _customerService.CreateCustomerAsync(form);
        CustomerForm = new();
        GetCustomers();
    }

    [RelayCommand]
    public async Task Delete(Customer customer)
    {
        var result = await _customerService.DeleteCustomerAsync(x => x.Id == customer.Id);
        if (result)
        {
            GetCustomers();
        }
    }

    [RelayCommand]
    public void AddToProject(Customer customer)
    {
        var projectAddViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
        projectAddViewModel.Form = ProjectForm;
        projectAddViewModel.Form.Customer.CustomerName = customer.CustomerName;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectAddViewModel;
    }

    [RelayCommand]
    public async Task GoToUpdate(Customer customer)
    {
        var result = await _customerService.GetCustomerAsync(x => x.Id == customer.Id);
        CustomerForm = CustomerFactory.CreateCustomerForm(result);
    }

    [RelayCommand]
    public async Task UpdateCustomer(CustomerRegistrationForm updatedCustomer)
    {
        await _customerService.UpdateCustomerAsync(x => x.Id == updatedCustomer.Id, CustomerFactory.Create(updatedCustomer));
        CustomerForm = new();
        GetCustomers();
    }

    public async void GetCustomers()
    {
        Customers = new ObservableCollection<Customer>(await _customerService.GetAllCustomersAsync());
    }
}
