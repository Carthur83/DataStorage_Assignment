using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IResult> CreateProjectAsync(ProjectRegistrationForm form)
    {
        if (form == null)
            return Result.BadRequest("Ogiltigt registreringsformulär");

        var customer = await _customerService.GetCustomerAsync(x => x.CustomerName == form.Customer.CustomerName);
        var employee = await _employeeService.GetEmployeeAsync(x => x.Id == form.ProjectManager.Id);
        var service = await _serviceService.GetServiceAsync(x => x.ServiceName == form.Service.ServiceName);
        var status = await _statusService.GetStatusAsync(form.CurrentStatus);

        if (customer == null)
        {
            var result = await _customerService.CreateCustomerAsync(form.Customer);
            if (result.Success)
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

            return Result.Ok();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _projectRepository.RollbackTransactionAsync();
            return Result.Error("Nåt gick fel, projekt ej skapat");
        }
    }

    public async Task<IEnumerable<Project>> GetAllProjectAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.Create);
        return projects;
    }

    public async Task<IResult> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        if (entity == null)
            return null!;

        var project = ProjectFactory.Create(entity);

        return Result<Project>.Ok(project);
    }

    public async Task<IResult> UpdateProjectAsync(Expression<Func<ProjectEntity, bool>> expression, Project updatedProject)
    {
        if (updatedProject == null)
            return null!;

        try
        {
            var existingEntity = await _projectRepository.GetAsync(expression);
            if (existingEntity == null)
                return Result.NotFound("Inget projekt hittades");

            var updatedEntity = ProjectFactory.Create(updatedProject);

            _projectRepository.Update(existingEntity, updatedEntity);
            await _projectRepository.SaveAsync();

            var project = ProjectFactory.Create(existingEntity);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, projekt ej uppdaterat");
        }

    }

    public async Task<IResult> DeleteProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _projectRepository.GetAsync(expression);
            if (entity == null)
                return Result.NotFound("Inget projekt hittades");

            _projectRepository.Delete(entity!);
            await _projectRepository.SaveAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, projekt ej raderat");
        }
    }

    public async Task<IResult> CheckIfExistsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var result = await _projectRepository.ExistsAsync(expression);
        return result ? Result.Ok() : Result.NotFound("Inget projekt hittades");
    }
}
