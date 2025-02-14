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

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<bool> CreateEmployeeAsync(EmployeeRegistrationForm form)
    {
        if (string.IsNullOrEmpty(form.FirstName) || string.IsNullOrEmpty(form.LastName))
            return false;

        var exists = await _employeeRepository.ExistsAsync(x => x.Id == form.Id);
        if (exists)
            return false;

        await _employeeRepository.BeginTransactionAsync();

        try
        {
            await _employeeRepository.CreateAsync(EmployeeFactory.CreateEntity(form));
            await _employeeRepository.SaveAsync();

            await _employeeRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _employeeRepository.RollbackTransactionAsync();
            return false;
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

    public async Task<Employee> UpdateEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression, Employee updatedEmployee)
    {
        if (updatedEmployee == null)
            return null!;

        try
        {
            var existingEntity = await _employeeRepository.GetAsync(expression);
            if (existingEntity == null)
                return null!;

            var updatedEntity = EmployeeFactory.CreateEntity(updatedEmployee);

            _employeeRepository.Update(existingEntity, updatedEntity);
            await _employeeRepository.SaveAsync();

            return EmployeeFactory.Create(existingEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }

    }

    public async Task<bool> DeleteEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var entity = await _employeeRepository.GetAsync(expression);
            _employeeRepository.Delete(entity!);
            await _employeeRepository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        return await _employeeRepository.ExistsAsync(expression);
    }
}
