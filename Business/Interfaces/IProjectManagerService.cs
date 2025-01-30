using Data.Entities;

namespace Business.Interfaces
{
    public interface IProjectManagerService
    {
        Task<ProjectManagerEntity> CreateAsync(ProjectManagerEntity entity);
    }
}