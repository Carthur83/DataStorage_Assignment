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

public class EmployeeService(IEmployeeRepository employeeRepository, IProjectRepository projectRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<IResult> CreateEmployeeAsync(EmployeeRegistrationForm form)
    {
        if (string.IsNullOrEmpty(form.EmploymentNumber) || string.IsNullOrEmpty(form.FirstName) || string.IsNullOrEmpty(form.LastName))
            return Result.BadRequest("Alla fält måste fyllas i!");

        var exists = await _employeeRepository.ExistsAsync(x => x.EmploymentNumber == form.EmploymentNumber);
        if (exists)
            return Result.AlreadyExists($"Anställd med anställningsnummer: {form.EmploymentNumber} finns redan");

        await _employeeRepository.BeginTransactionAsync();

        try
        {
            await _employeeRepository.CreateAsync(EmployeeFactory.CreateEntity(form));
            await _employeeRepository.SaveAsync();

            await _employeeRepository.CommitTransactionAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _employeeRepository.RollbackTransactionAsync();
            return Result.Error("Nåt gick fel, försök igen"); ;
        }
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        var entities = await _employeeRepository.GetAllAsync();
        var employees = entities.Select(EmployeeFactory.Create);
        return employees;
    }

    public async Task<Employee> GetEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        var entity = await _employeeRepository.GetAsync(expression);
        if (entity == null)
            return null!;

        var employee = EmployeeFactory.Create(entity);
        return employee;
    }

    public async Task<IResult> UpdateEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression, Employee updatedEmployee)
    {
        if (updatedEmployee == null)
            return null!;

        try
        {
            var existingEntity = await _employeeRepository.GetAsync(expression);
            if (existingEntity == null)
                return Result.NotFound("Ingen anställd hittades");

            var updatedEntity = EmployeeFactory.CreateEntity(updatedEmployee);

            _employeeRepository.Update(existingEntity, updatedEntity);
            await _employeeRepository.SaveAsync();

            var employee = EmployeeFactory.Create(existingEntity);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, ej uppdaterat");
        }

    }

    public async Task<IResult> DeleteEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _employeeRepository.GetAsync(expression);
            if (entity == null)
                return Result.NotFound("Ingen anställd hittades");

            var existsInProject = await _projectRepository.ExistsAsync(x => x.EmployeeId == entity.Id);
            if (existsInProject)
                return Result.BadRequest("Anställd används i projekt, går ej ta bort");

            _employeeRepository.Delete(entity!);
            await _employeeRepository.SaveAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, ej uppdaterat");
        }
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        return await _employeeRepository.ExistsAsync(expression);
    }
}
