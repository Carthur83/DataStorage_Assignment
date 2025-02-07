using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ServiceFactory
{
    public static Service Create(ServiceEntity entity)
    {
        return new Service()
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            Price = entity.Price,
        };
    }

    public static ProjectRegistrationForm Create(Service service)
    {
        return new ProjectRegistrationForm()
        {
            ServiceName = service.ServiceName,
            Price = service.Price,
        };
    }
}
