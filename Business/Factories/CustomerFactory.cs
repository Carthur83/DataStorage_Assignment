using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity CreateEntity(CustomerRegistrationForm form)
    {
        return new CustomerEntity
        {
            CustomerName = form.CustomerName,
        };
    }
    public static Customer Create(CustomerEntity entity)
    {
        return new Customer
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
        };
    }

    public static CustomerEntity Create(Customer customer)
    {
        return new CustomerEntity
        {
            Id = customer.Id,
            CustomerName = customer.CustomerName,
        };
    }

    public static CustomerRegistrationForm CreateCustomerForm(Customer customer)
    {
        return new CustomerRegistrationForm
        {
            Id = customer.Id,
            CustomerName = customer.CustomerName,
        };
    }

    public static Customer Create(CustomerRegistrationForm form)
    {
        return new Customer
        {
            Id = form.Id,
            CustomerName = form.CustomerName,
        };
    }
}
