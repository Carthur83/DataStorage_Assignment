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
public class ServiceService(IServiceRepository serviceRepository, IProjectRepository projectRepository) : IServiceService
{

    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IProjectRepository _projectRepository = projectRepository;
    public async Task<IResult> CreateServiceAsync(ServiceRegistrationForm form)
    {
        if (string.IsNullOrEmpty(form.ServiceName))
            return Result.BadRequest("Alla fält måste fyllas i");

        var exists = await _serviceRepository.ExistsAsync(x => x.ServiceName == form.ServiceName);

        if (exists)
            return Result.AlreadyExists("Tjänst finns redan");

        await _serviceRepository.BeginTransactionAsync();

        try
        {
            await _serviceRepository.CreateAsync(ServiceFactory.CreateEntity(form));
            await _serviceRepository.SaveAsync();

            await _serviceRepository.CommitTransactionAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await _serviceRepository.RollbackTransactionAsync();
            return Result.Error("Nåt gick fel, försök igen");
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

    public async Task<IResult> UpdateServiceAsync(Expression<Func<ServiceEntity, bool>> expression, Service updatedService)
    {
        if (updatedService == null)
            return null!;

        try
        {
            var existingEntity = await _serviceRepository.GetAsync(expression);
            if (existingEntity == null)
                return Result.NotFound("Ingen tjänst hittades");

            var updatedEntity = ServiceFactory.CreateEntity(updatedService);

            _serviceRepository.Update(existingEntity, updatedEntity);
            await _serviceRepository.SaveAsync();

            var service = ServiceFactory.Create(existingEntity);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, ej uppdaterat");
        }
    }

    public async Task<IResult> DeleteServiceAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _serviceRepository.GetAsync(expression);
            if (entity == null)
                return Result.NotFound("Ingen tjänst hittades");

            var existsInProject = await _projectRepository.ExistsAsync(x => x.ServiceId == entity.Id);
            if (existsInProject)
                return Result.BadRequest("Tjänst används i projekt, går ej ta bort");

            _serviceRepository.Delete(entity!);
            await _serviceRepository.SaveAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, försök igen");
        }
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        return await _serviceRepository.ExistsAsync(expression);
    }
}
