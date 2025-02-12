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

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateCustomerAsync(string customerName)
    {
        var exists = await _customerRepository.ExistsAsync(x => x.CustomerName == customerName);
        if (exists)
            return false;
        try
        {
            var customerEntity = new CustomerEntity
            {
                CustomerName = customerName,
            };

            await _customerRepository.CreateAsync(customerEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.CreateEntity);
        return customers;
    }

    public async Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var entity = await _customerRepository.GetAsync(expression);
        if (entity == null)
            return null!;

        var customer = CustomerFactory.CreateEntity(entity);
        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(Expression<Func<CustomerEntity, bool>> expression, Customer updatedCustomer)
    {
        var updatedEntity = CustomerFactory.Create(updatedCustomer);
        var entity =  await _customerRepository.UpdateAsync(expression, updatedEntity);
        return CustomerFactory.CreateEntity(entity);
    }

    public async Task<bool> DeleteCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var result = await _customerRepository.DeleteAsync(expression);
        return result;
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        return await _customerRepository.ExistsAsync(expression);
    }

}
