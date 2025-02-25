﻿using Business.Dtos;
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

    public async Task<IResult> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        if (string.IsNullOrEmpty(form.CustomerName)) 
            return Result.BadRequest("Fältet får inte vara tomt");

        var exists = await _customerRepository.ExistsAsync(x => x.CustomerName == form.CustomerName);
        if (exists)
            return Result.AlreadyExists("Kund finns redan");

        await _customerRepository.BeginTransactionAsync();

        try
        {
            await _customerRepository.CreateAsync(CustomerFactory.CreateEntity(form));
            await _customerRepository.SaveAsync();

            await _customerRepository.CommitTransactionAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _customerRepository.RollbackTransactionAsync();
            return Result.Error("Nåt gick fel, försök igen");
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

    public async Task<IResult> UpdateCustomerAsync(Expression<Func<CustomerEntity, bool>> expression, Customer updatedCustomer)
    {
        if (updatedCustomer == null)
            return null!;

        try
        {
            var existingEntity = await _customerRepository.GetAsync(expression);
            if (existingEntity == null)
                return Result.NotFound("Ingen kund hittades");

            var updatedEntity = CustomerFactory.Create(updatedCustomer);

            _customerRepository.Update(existingEntity, updatedEntity);
            await _customerRepository.SaveAsync();

            var customer = CustomerFactory.Create(existingEntity);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, kund ej uppdaterad");
        }

    }

    public async Task<IResult> DeleteCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _customerRepository.GetAsync(expression);
            if (entity == null)
                return Result.NotFound("Ingen kund hittades");

            _customerRepository.Delete(entity!);
            await _customerRepository.SaveAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, kund ej uppdaterad");
        }
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        return await _customerRepository.ExistsAsync(expression);
    }

}
