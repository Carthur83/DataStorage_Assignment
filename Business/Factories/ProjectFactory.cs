using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Factories;

public class ProjectFactory(ICustomerService customerService, IEmployeeService employeeService, IServiceService serviceService, IStatusService statusService) : IProjectFactory
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IServiceService _serviceService = serviceService;
    private readonly IStatusService _statusService = statusService;

    public async Task<ProjectEntity> Create(ProjectRegistrationForm form)
    {
        var customerEntity = await _customerService.CreateCustomerAsync(form.CustomerName);
        var employeeEntity = await _employeeService.CreateAsync(form.ProjectManager);
        var serviceEntity = await _serviceService.CreateServiceAsync(form.ServiceName, form.Price);
        var statusEntity = await _statusService.GetStatusAsync(form.CurrentStatus);

        return new ProjectEntity
        {
            ProjectName = form.ProjectName,
            StartDate = DateTime.Parse(form.StartDate),
            EndDate = DateTime.Parse(form.EndDate),
            TotalPrice = form.TotalPrice,
            Customer = customerEntity,
            Status = statusEntity,
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
            TotalPrice = entity.TotalPrice,
            CustomerId = entity.CustomerId,
            CustomerName = entity.Customer.CustomerName,
            StatusId = entity.StatusId,
            StatusType = entity.Status.StatusType,
            EmployeeId = entity.EmployeeId,
            EmployeeFirstName = entity.Employee.FirstName,
            EmployeeLastName = entity.Employee.LastName,
            ProjectManager = entity.Employee.FirstName + " " + entity.Employee.LastName,
            ServiceId = entity.ServiceId,
            ServiceName = entity.Service.ServiceName,
            Price = entity.Service.Price,
        };
    }

    public async Task<ProjectEntity> Create(Project project)
    {
        var serviceEntity = await _serviceService.CreateServiceAsync(project.ServiceName, project.Price);

        return new ProjectEntity
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CustomerId = project.CustomerId,
            StatusId= project.StatusId,
            EmployeeId= project.EmployeeId,
            ServiceId= serviceEntity.Id,
            TotalPrice = project.TotalPrice,
            Customer = new CustomerEntity { Id = project.CustomerId, CustomerName = project.CustomerName },
            Status = new StatusEntity { Id = project.StatusId, StatusType = project.StatusType },
            Employee = new EmployeeEntity { Id = project.EmployeeId, FirstName = project.EmployeeFirstName, LastName = project.EmployeeLastName },
            Service = serviceEntity
        };
    }
}
