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

    [ObservableProperty]
    private Project _selectedProject;

    public ProjectListViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        GetProjects();
    }

    // metoden är från chatgpt
    // den anropas när man klickar på en rad i listview och i sin tur
    // anropar den GoToEditView och skickar med radens objekt
    partial void OnSelectedProjectChanged(Project? value)
    {
        if (value != null)
        {
            GoToEditViewCommand.Execute(value);
        }
    }

    [RelayCommand]
    private void GoToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectAddViewModel>();
    }

    [RelayCommand]
    public void GoToEditView(Project project)
    {
        var projectEditViewModel = _serviceProvider.GetRequiredService<ProjectEditViewModel>();
        projectEditViewModel.Project = project;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = projectEditViewModel;
    }

    public async void GetProjects()
    {
        Projects = new ObservableCollection<Project>(await _projectService.GetAllProjectAsync());
    }


}
