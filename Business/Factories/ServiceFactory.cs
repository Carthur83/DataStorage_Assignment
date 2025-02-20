using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ServiceFactory
{
    public static ServiceEntity CreateEntity(ServiceRegistrationForm form)
    {
        return new ServiceEntity
        {
            ServiceName = form.ServiceName,
            Price = form.Price,
        };

    }
    public static Service Create(ServiceEntity entity)
    {
        return new Service()
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            Price = entity.Price,
        };
    }

    public static ProjectRegistrationForm CreateProjectForm(Service service)
    {
        return new ProjectRegistrationForm()
        {
            Service = new ServiceRegistrationForm { ServiceName = service.ServiceName, Price = service.Price },
        };
    }

    public static ServiceRegistrationForm CreateServiceForm(ServiceEntity entity)
    {
        return new ServiceRegistrationForm
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            Price = entity.Price,
        };
    }

    public static Service Create(ServiceRegistrationForm form)
    {
        return new Service
        {
            Id = form.Id,
            ServiceName = form.ServiceName,
            Price = form.Price,
        };
    }
    public static ServiceEntity CreateEntity(Service service)
    {
        return new ServiceEntity
        {
            Id = service.Id,
            ServiceName = service.ServiceName,
            Price = service.Price,
        };
    }
}
