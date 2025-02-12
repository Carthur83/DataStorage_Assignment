using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<bool> CreateServiceAsync(ServiceRegistrationForm form);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service> GetServiceAsync(Expression<Func<ServiceEntity, bool>> expression);
        Task<Service> UpdateServiceAsync(Expression<Func<ServiceEntity, bool>> expression, Service updatedService);
        Task<bool> DeleteServiceAsync(Expression<Func<ServiceEntity, bool>> expression);
    }
}