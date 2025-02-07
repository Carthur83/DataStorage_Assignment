using Business.Dtos;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_Wpf.ViewModels;

public partial class ProjectAddViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;
    private readonly IStatusService _statusService;
    private readonly IServiceService _serviceService;

    [ObservableProperty]
    private ProjectRegistrationForm _form = new();

    [ObservableProperty]
    private ObservableCollection<StatusEntity> _statuses = [];

    public ProjectAddViewModel(IServiceProvider serviceProvider, IProjectService projectService, IStatusService statusService, IServiceService serviceService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        _statusService = statusService;
        _serviceService = serviceService;
        InitializeAsync();
    }

    public async void InitializeAsync()
    {
       await GetStatuses();
    }

    [RelayCommand]
    public async Task Save()
    {
        var result = await _projectService.CreateProjectAsync(Form);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
        }
    }

    [RelayCommand]
    public void GoToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
    }

    [RelayCommand]
    public void GoToServiceView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ServiceListViewModel>();
    }

    public async Task GetStatuses()
    {
      Statuses = new ObservableCollection<StatusEntity>(await _statusService.GetAllStatusesAsync());
    }

   

}
