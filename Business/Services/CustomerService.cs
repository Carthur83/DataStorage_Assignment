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

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var exists = await _customerRepository.ExistsAsync(x => x.CustomerName == form.CustomerName);
        if (exists)
            return false;

        await _customerRepository.BeginTransactionAsync();

        try
        {
            await _customerRepository.CreateAsync(CustomerFactory.CreateEntity(form));
            await _customerRepository.SaveAsync();

            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _customerRepository.RollbackTransactionAsync();
            return false;
        }
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create);
        return customers;
    }

    public async Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var entity = await _customerRepository.GetAsync(expression);
        if (entity == null)
            return null!;

        var customer = CustomerFactory.Create(entity);
        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(Expression<Func<CustomerEntity, bool>> expression, Customer updatedCustomer)
    {
        if (updatedCustomer == null)
            return null!;

        try
        {
            var existingEntity = await _customerRepository.GetAsync(expression);
            if (existingEntity == null)
                return null!;

            var updatedEntity = CustomerFactory.Create(updatedCustomer);

            _customerRepository.Update(existingEntity, updatedEntity);
            await _customerRepository.SaveAsync();

            return CustomerFactory.Create(existingEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }

    }

    public async Task<bool> DeleteCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var entity = await _customerRepository.GetAsync(expression);
            _customerRepository.Delete(entity!);
            await _customerRepository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        return await _customerRepository.ExistsAsync(expression);
    }

}
