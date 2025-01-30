using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository repository, IProjectFactory factory) : IProjectService
{
    private readonly IProjectRepository _repository = repository;
    private readonly IProjectFactory _projectFactory = factory;
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        try
        {
            var entity = await _repository.GetAsync(x => x.ProjectName == form.ProjectName);
            if (entity == null)
            {
                entity = await _repository.CreateAsync(await _projectFactory.Create(form));
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetProjectsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Project> UpdateProjectAsync(Project updatedProject)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProjectAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckIfExistsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }
}
