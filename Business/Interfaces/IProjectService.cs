using Business.Dtos;
using Business.Models;
using Data.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<Project>> GetAllProjectAsync();
    Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<Project> UpdateProjectAsync(Expression<Func<ProjectEntity, bool>> expression, Project updatedProject);
    Task<bool> DeleteProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<bool> CheckIfExistsAsync(Expression<Func<ProjectEntity, bool>> expression);
}
