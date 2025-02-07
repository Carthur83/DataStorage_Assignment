using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IProjectFactory
    {
        Project Create(ProjectEntity entity);
        Task<ProjectEntity> Create(ProjectRegistrationForm form);
        Task<ProjectEntity> Create(Project updatedProject);
    }
}