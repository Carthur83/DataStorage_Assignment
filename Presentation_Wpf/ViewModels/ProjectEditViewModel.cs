using Business.Interfaces;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_Wpf.ViewModels;

public partial class ProjectEditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;
    private readonly IStatusService _statusService;

    public ProjectEditViewModel(IServiceProvider serviceProvider, IProjectService projectService, IStatusService statusService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        _statusService = statusService;
        GetStatuses();
    }

    [ObservableProperty]
    private Project _project = new();

    [ObservableProperty]
    private ObservableCollection<StatusEntity> _statuses = [];
    [RelayCommand]
    public void GoToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
    }

    [RelayCommand]
    public async Task Save()
    {
        Project.StatusId = await _statusService.GetStatusIdAsync(Project.StatusType);

        var result = await _projectService.UpdateProjectAsync(x => x.Id == Project.Id, Project);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
    }

    [RelayCommand]
    public async Task DeleteProject(Project project)
    {
        var result = await _projectService.DeleteProjectAsync(x => x.Id == project.Id);

        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
        }
    }

    public async void GetStatuses()
    {
        Statuses = new ObservableCollection<StatusEntity>(await _statusService.GetAllStatusesAsync());
    }
}
