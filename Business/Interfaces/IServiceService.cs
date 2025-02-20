using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<IResult> CreateServiceAsync(ServiceRegistrationForm form);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<ServiceEntity> GetServiceAsync(Expression<Func<ServiceEntity, bool>> expression);
        Task<IResult> UpdateServiceAsync(Expression<Func<ServiceEntity, bool>> expression, Service updatedService);
        Task<IResult> DeleteServiceAsync(Expression<Func<ServiceEntity, bool>> expression);
    }
}