using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerRegistrationForm form);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<Customer> UpdateCustomerAsync(Expression<Func<CustomerEntity, bool>> expression, Customer updatedCustomer);
        Task<bool> DeleteCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<bool> CheckIfExistsAsync(Expression<Func<CustomerEntity, bool>> expression);
    }
}