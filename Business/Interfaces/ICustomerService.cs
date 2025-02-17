using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> CreateCustomerAsync(CustomerRegistrationForm form);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<IResult> UpdateCustomerAsync(Expression<Func<CustomerEntity, bool>> expression, Customer updatedCustomer);
        Task<IResult> DeleteCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<bool> CheckIfExistsAsync(Expression<Func<CustomerEntity, bool>> expression);
    }
}