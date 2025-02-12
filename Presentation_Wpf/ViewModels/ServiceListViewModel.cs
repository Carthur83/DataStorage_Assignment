using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_Wpf.ViewModels;

public partial class ServiceListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceService _serviceService;

    [ObservableProperty]
    private ObservableCollection<Service> _services = [];

    [ObservableProperty]
    private ServiceRegistrationForm _serviceForm = new();

    [ObservableProperty]
    private Service _service = new();

    [ObservableProperty]
    private ProjectRegistrationForm _projectForm = new();

    public ServiceListViewModel(IServiceProvider serviceProvider, IServiceService serviceService)
    {
        _serviceProvider = serviceProvider;
        _serviceService = serviceService;
        GetServices();
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
    public async Task AddService(ServiceRegistrationForm serviceForm)
    {
        var result = await _serviceService.CreateServiceAsync(serviceForm);
        GetServices();
    }

    [RelayCommand]
    public async Task Delete(Service service)
    {
        var result = await _serviceService.DeleteServiceAsync(x => x.ServiceName == service.ServiceName);
        if (result)
        {
            GetServices();
        }
    }

    [RelayCommand]
    public void AddToProject(Service service)
    {
        var projectAddViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
        projectAddViewModel.Form = ProjectForm;
        projectAddViewModel.Form.Service.ServiceName = service.ServiceName;
        projectAddViewModel.Form.Service.Price = service.Price;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectAddViewModel;
    }

    [RelayCommand]  
    public async Task GoToUpdate(Service service)
    {
        var result = await _serviceService.GetServiceAsync(x => x.Id == service.Id);
        ServiceForm = ServiceFactory.CreateServiceForm(result);
    }

    [RelayCommand]
    public async Task UpdateService(ServiceRegistrationForm updatedService)
    {
        await _serviceService.UpdateServiceAsync(x => x.Id == updatedService.Id, ServiceFactory.Create(updatedService));
        ServiceForm = new();
        GetServices();
    }

    public async void GetServices()
    {
        Services = new ObservableCollection<Service>(await _serviceService.GetAllServicesAsync());
    }
}
