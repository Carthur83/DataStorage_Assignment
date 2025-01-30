using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ProjectFactory(ICustomerService customerService, IEmployeeService employeeService, IServiceService serviceService) : IProjectFactory
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IServiceService _serviceService = serviceService;

    public ProjectRegistrationForm Create() => new();

    public async Task<ProjectEntity> Create(ProjectRegistrationForm form)
    {
        var customerEntity = await _customerService.CreateCustomerAsync(form.CustomerName);
        var employeeEntity = await _employeeService.CreateAsync(form.ProjectManager);
        var serviceEntity = await _serviceService.CreateCustomerAsync(form.ServiceName);

        return new ProjectEntity
        {
            ProjectName = form.ProjectName,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Customer = customerEntity,
            StatusId = 1,
            Employee = employeeEntity,
            Service = serviceEntity,
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
            ProjectManager = entity.Employee.FirstName + " " + entity.Employee.LastName,
            ServiceName = entity.Service.ServiceName
        };
    }
}
