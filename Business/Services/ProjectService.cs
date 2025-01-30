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
    private readonly IProjectRepository _projectRepository = repository;
    private readonly IProjectFactory _projectFactory = factory;
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        try
        {
            var entity = await _projectRepository.GetAsync(x => x.ProjectName == form.ProjectName);
            if (entity == null)
            {
                entity = await _projectFactory.Create(form);
                await _projectRepository.CreateAsync(entity);
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

    public async Task<IEnumerable<Project>> GetAllProjectAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(_projectFactory.Create);
        return projects;
    }

    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        var project = _projectFactory.Create(entity);

        return project;
    }

    public async Task<Project> UpdateProjectAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedProject)
    {
        var entity = await _projectRepository.UpdateAsync(expression, updatedProject);
        var project = _projectFactory.Create(updatedProject);

        return project;
    }

    public async Task<bool> DeleteProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var result = await _projectRepository.DeleteAsync(expression);
        return result;
    }

    public async Task<bool> CheckIfExistsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return await _projectRepository.ExistsAsync(expression);
    }
}
