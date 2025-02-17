using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_Wpf.ViewModels;

public partial class ServiceEditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceService _serviceService;

    public ServiceEditViewModel(IServiceProvider serviceProvider, IServiceService serviceService)
    {
        _serviceProvider = serviceProvider;
        _serviceService = serviceService;
        GetServices();
    }

    [ObservableProperty]
    private ObservableCollection<Service> _services = [];

    [ObservableProperty]
    private ServiceRegistrationForm _serviceForm = new();

    [ObservableProperty]
    private Service _service = new();

    [ObservableProperty]
    private Project _project = new();

    [ObservableProperty]
    private string? _message;

    [ObservableProperty]
    private string? _deleteMessage;

    [RelayCommand]
    public void GoToProjectEditView(Project project)
    {
        var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();
        projectEditViewModel.Project = project;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectEditViewModel;
    }

    [RelayCommand]
    public async Task AddService(ServiceRegistrationForm serviceForm)
    {
        var result = await _serviceService.CreateServiceAsync(serviceForm);
        if (result.Success)
        {
            GetServices();
        }
        Message = result.ErrorMessage!;
    }

    [RelayCommand]
    public void GoToUpdate(Service service)
    {
        ServiceForm = ServiceFactory.CreateServiceForm(service);
    }

    [RelayCommand]
    public async Task UpdateService(ServiceRegistrationForm updatedService)
    {
        await _serviceService.UpdateServiceAsync(x => x.Id == updatedService.Id, ServiceFactory.Create(updatedService));
        ServiceForm = new();
        GetServices();
    }

    [RelayCommand]
    public void AddToProject(Service service)
    {
        var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();

        projectEditViewModel.Project = Project;
        projectEditViewModel.Project.ServiceId = service.Id;
        projectEditViewModel.Project.ServiceName = service.ServiceName;
        projectEditViewModel.Project.Price = service.Price;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectEditViewModel;
    }

    [RelayCommand]
    public async Task Delete(Service service)
    {
        var result = await _serviceService.DeleteServiceAsync(x => x.ServiceName == service.ServiceName);
        if (result.Success)
        {
            GetServices();
        }
        DeleteMessage = result.ErrorMessage!;
    }
    public async void GetServices()
    {
        Services = new ObservableCollection<Service>(await _serviceService.GetAllServicesAsync());
    }
}
