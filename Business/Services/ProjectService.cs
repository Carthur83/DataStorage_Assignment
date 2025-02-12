using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository repository, IServiceService serviceService, IEmployeeService employeeService, ICustomerService customerService, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = repository;
    private readonly IServiceService _serviceService = serviceService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IStatusService _statusService = statusService;
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var service = await _serviceService.GetServiceAsync(x => x.ServiceName == form.Service.ServiceName);
        var employee = await _employeeService.GetEmployeeAsync(x => x.Id == form.ProjectManager.Id);
        var customer = await _customerService.GetCustomerAsync(x => x.CustomerName == form.Customer.CustomerName);
        var status = await _statusService.GetStatusAsync(form.CurrentStatus);

        if (customer == null)
        {
            var result = await _customerService.CreateCustomerAsync(form.Customer);
            if (result)
                customer = await _customerService.GetCustomerAsync(x => x.CustomerName == form.Customer.CustomerName);
        }

        if (employee == null)
        {
            var result = await _employeeService.CreateEmployeeAsync(form.ProjectManager);
            if (result)
                employee = await _employeeService.GetEmployeeAsync(x => x.Id == form.ProjectManager.Id);
        }

        if (service == null)
        {
            var result = await _serviceService.CreateServiceAsync(form.Service);
            if (result)
                service = await _serviceService.GetServiceAsync(x => x.ServiceName == form.Service.ServiceName);
        }

        await _projectRepository.BeginTransactionAsync();

        try
        {
            var projectEntity = ProjectFactory.Create(form);
            projectEntity.CustomerId = customer!.Id;
            projectEntity.ServiceId = service!.Id;
            projectEntity.StatusId = status!.Id;
            projectEntity.EmployeeId = employee!.Id;

            await _projectRepository.CreateAsync(projectEntity);
            await _projectRepository.SaveAsync();

            await _projectRepository.CommitTransactionAsync();

            return true;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _projectRepository.RollbackTransactionAsync();
            return false;
        }
    }

    public async Task<IEnumerable<Project>> GetAllProjectAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.Create);
        return projects;
    }

    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        if (entity == null)
            return null!;

        var project = ProjectFactory.Create(entity);

        return project;
    }

    public async Task<Project> UpdateProjectAsync(Expression<Func<ProjectEntity, bool>> expression, Project updatedProject)
    {
        if (updatedProject == null)
            return null!;

        try
        {
            var existingEntity = await _projectRepository.GetAsync(expression);
            if (existingEntity == null)
                return null!;

            var updatedEntity = ProjectFactory.Create(updatedProject);

            _projectRepository.Update(existingEntity, updatedEntity);
            await _projectRepository.SaveAsync();

            var project = ProjectFactory.Create(existingEntity);
            return project;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }

    }

    public async Task<bool> DeleteProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var entity = await _projectRepository.GetAsync(expression);
            _projectRepository.Delete(entity!);
            await _projectRepository.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return await _projectRepository.ExistsAsync(expression);
    }
}
