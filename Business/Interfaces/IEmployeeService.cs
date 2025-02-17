using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Services;

public interface IEmployeeService
{
    Task<IResult> CreateEmployeeAsync(EmployeeRegistrationForm form);
    Task<Employee> GetEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<IResult> UpdateEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression, Employee updatedEmployee);
    Task<IResult> DeleteEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression);
    Task<bool> CheckIfExistsAsync(Expression<Func<EmployeeEntity, bool>> expression);
}