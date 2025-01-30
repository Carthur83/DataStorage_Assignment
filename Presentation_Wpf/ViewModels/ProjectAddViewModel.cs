using Business.Dtos;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Entities;

namespace Presentation_Wpf.ViewModels;

public partial class ProjectAddViewModel(IServiceProvider serviceProvider, IProjectFactory projectFactory, IProjectService projectService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IProjectFactory _projectFactory = projectFactory;
    private readonly IProjectService _projectService = projectService;

    [ObservableProperty]
    private ProjectRegistrationForm? _form = new();

    [RelayCommand]
    public async Task AddProject()
    {
        var result = await _projectService.CreateProjectAsync(Form);

    }
    
}
