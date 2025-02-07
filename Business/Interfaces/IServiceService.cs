using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceEntity> CreateServiceAsync(string serviceName, decimal price);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<bool> DeleteServiceAsync(Expression<Func<ServiceEntity, bool>> expression);
    }
}