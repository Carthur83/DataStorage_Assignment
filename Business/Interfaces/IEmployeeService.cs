using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Services;

public interface IEmployeeService
{
    Task<bool> CreateEmployeeAsync(EmployeeRegistrationForm form);
    Task<Employee> GetEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> UpdateEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression, Employee updatedEmployee);
    Task<bool> DeleteEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression);
    Task<bool> CheckIfExistsAsync(Expression<Func<EmployeeEntity, bool>> expression);
}