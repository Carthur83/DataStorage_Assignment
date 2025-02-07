using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;
namespace Business.Services;
public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{

    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<ServiceEntity> CreateServiceAsync(string serviceName, decimal price)
    {
        var serviceEntity = await _serviceRepository.GetAsync(x => x.ServiceName == serviceName);
        if (serviceEntity == null)
        {
            serviceEntity = new ServiceEntity
            {
                ServiceName = serviceName,
                Price = price            
            };

            await _serviceRepository.CreateAsync(serviceEntity);
        } 
 
        return serviceEntity;
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        var serviceEntities = await _serviceRepository.GetAllAsync();
        var services = serviceEntities.Select(ServiceFactory.Create);
        return services;
    }

    public async Task<bool> DeleteServiceAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        var result = await _serviceRepository.DeleteAsync(expression);
        return result;
    }
}
