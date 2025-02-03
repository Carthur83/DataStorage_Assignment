using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation_Wpf.ViewModels;

public partial class ProjectListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    [ObservableProperty]
    private ObservableCollection<Project> _projects = [];

    public ProjectListViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        GetProjects();
    }

    [RelayCommand]
    private void GoToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
    }

    public async void GetProjects()
    {
        Projects = new ObservableCollection<Project>(await _projectService.GetAllProjectAsync());
    }
}
