using Data.Entities;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerEntity> CreateCustomerAsync(string customerName);
    }
}