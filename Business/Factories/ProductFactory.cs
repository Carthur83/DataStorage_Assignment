using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ProjectFactory(ICustomerService customerService, IProjectManagerService projectManagerService) : IProjectFactory
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectManagerService _projectManagerService = projectManagerService;

    public ProjectRegistrationForm Create() => new();

    public async Task<ProjectEntity> Create(ProjectRegistrationForm form)
    {
        var customerEntity = await _customerService.CreateCustomerAsync(form.CustomerName);
        var projectManagerEntity = await _projectManagerService.CreateAsync(form.ProjectManager);

        return new ProjectEntity
        {
            ProjectName = form.ProjectName,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Customer = customerEntity,
            StatusId = 1,
            ProjectManager = projectManagerEntity,

        };
    }

    public Project Create(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CustomerName = entity.Customer.CustomerName,
            CurrentStatus = entity.Status.StatusType,
            ProjectManager = entity.ProjectManager.FirstName + " " + entity.ProjectManager.LastName,
        };
    }
}
