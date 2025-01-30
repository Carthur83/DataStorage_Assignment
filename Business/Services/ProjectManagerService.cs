using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectManagerService(IProjectManagerRepository repository) : IProjectManagerService
{
    private readonly IProjectManagerRepository _repository = repository;

    public async Task<ProjectManagerEntity> CreateAsync(ProjectManagerEntity entity)
    {
        var projectManagerEntity = await _repository.GetAsync(x => x.Id == entity.Id);
        if (projectManagerEntity == null)
        {
            projectManagerEntity = new ProjectManagerEntity
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };

            await _repository.CreateAsync(projectManagerEntity);

        }

        return projectManagerEntity;
    }
}
