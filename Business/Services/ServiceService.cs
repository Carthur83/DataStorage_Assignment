using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
namespace Business.Services;
public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{

    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<ServiceEntity> CreateCustomerAsync(string serviceName)
    {
        var serviceEntity = await _serviceRepository.GetAsync(x => x.ServiceName == serviceName);
        if (serviceEntity == null)
        {
            serviceEntity = new ServiceEntity
            {
                ServiceName = serviceName,
            };

            await _serviceRepository.CreateAsync(serviceEntity);

        }

        return serviceEntity;
    }
}
