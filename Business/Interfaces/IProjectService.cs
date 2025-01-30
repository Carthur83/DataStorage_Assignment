using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<Project>> GetProjectsAsync();
    Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<Project> UpdateProjectAsync(Project updatedProject);
    Task<bool> DeleteProjectAsync(int id);
    Task<bool> CheckIfExistsAsync(Expression<Func<ProjectEntity, bool>> expression);
}
