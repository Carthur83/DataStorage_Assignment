using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(ProjectRegistrationForm form)
    {
        return new ProjectEntity
        {
            ProjectName = form.ProjectName,
            StartDate = DateTime.Parse(form.StartDate),
            EndDate = DateTime.Parse(form.EndDate),
            TotalPrice = form.TotalPrice,         
        };
    }

    public static Project Create(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TotalPrice = entity.TotalPrice,
            CustomerId = entity.CustomerId,
            CustomerName = entity.Customer.CustomerName,
            StatusId = entity.StatusId,
            StatusType = entity.Status.StatusType,
            EmployeeId = entity.EmployeeId,
            EmployementNumber = entity.Employee.EmploymentNumber,
            EmployeeFirstName = entity.Employee.FirstName,
            EmployeeLastName = entity.Employee.LastName,
            ProjectManager = entity.Employee.FirstName + " " + entity.Employee.LastName,
            ServiceId = entity.ProjectService.ServiceId,
            ServiceName = entity.ProjectService.Service.ServiceName,
            Price = entity.ProjectService.Price
        };
    }

    public static ProjectEntity Create(Project project)
    {
        return new ProjectEntity
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CustomerId = project.CustomerId,
            StatusId = project.StatusId,
            EmployeeId = project.EmployeeId,
            TotalPrice = project.TotalPrice,
            Customer = new CustomerEntity { Id = project.CustomerId, CustomerName = project.CustomerName },
            Status = new StatusEntity { Id = project.StatusId, StatusType = project.StatusType },
            Employee = new EmployeeEntity { Id = project.EmployeeId, EmploymentNumber = project.EmployementNumber, FirstName = project.EmployeeFirstName, LastName = project.EmployeeLastName },
        };
    }
}
