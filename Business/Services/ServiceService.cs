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
public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{

    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<bool> CreateServiceAsync(ServiceRegistrationForm form)
    {
        var exists = await _serviceRepository.ExistsAsync(x => x.ServiceName == form.ServiceName);

        if (exists)
            return false;
           
        try
        {
            await _serviceRepository.CreateAsync(ServiceFactory.CreateEntity(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        var serviceEntities = await _serviceRepository.GetAllAsync();
        var services = serviceEntities.Select(ServiceFactory.Create);
        return services;
    }

    public async Task<Service> GetServiceAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        var entity = await _serviceRepository.GetAsync(expression);
        if (entity == null)
            return null!;

        var service = ServiceFactory.Create(entity);
        return service;
    }

    public async Task<Service> UpdateServiceAsync(Expression<Func<ServiceEntity, bool>> expression, Service updatedService)
    {
        var updatedEntity = ServiceFactory.CreateEntity(updatedService);
        var entity = await _serviceRepository.UpdateAsync(expression, updatedEntity);
        var project = ServiceFactory.Create(entity);

        return project;
    }

    public async Task<bool> DeleteServiceAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        var result = await _serviceRepository.DeleteAsync(expression);
        return result;
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        return await _serviceRepository.ExistsAsync(expression);
    }
}
