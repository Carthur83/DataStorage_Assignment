using Data.Entities;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceEntity> CreateCustomerAsync(string serviceName);
    }
}