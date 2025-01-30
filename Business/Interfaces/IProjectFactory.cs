using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IProjectFactory
    {
        ProjectRegistrationForm Create();
        Project Create(ProjectEntity entity);
        Task<ProjectEntity> Create(ProjectRegistrationForm form);
    }
}