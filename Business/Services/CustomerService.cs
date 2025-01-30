using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerEntity> CreateCustomerAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        if (customerEntity == null)
        {
            customerEntity = new CustomerEntity
            {
                CustomerName = customerName,
            };

            await _customerRepository.CreateAsync(customerEntity);

        }

        return customerEntity;
    }
}
