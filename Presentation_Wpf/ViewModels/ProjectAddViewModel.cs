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

    [ObservableProperty]
    private ProjectRegistrationForm _form = new();

    [ObservableProperty]
    private ObservableCollection<string> _statuses = ["Ej påbörjad", "Pågående", "Slutförd"];

    public ProjectAddViewModel(IServiceProvider serviceProvider, IProjectService projectService, IStatusService statusService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        _statusService = statusService;
    }

    [RelayCommand]
    public async Task AddProject()
    {
        var result = await _projectService.CreateProjectAsync(Form);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectListViewModel>();
        }

    }

    public void GetStatuses()
    {
      //  Statuses = new ObservableCollection<StatusEntity>(await _statusService.GetAllStatusesAsync());
    }

}
