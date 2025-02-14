using Business.Dtos;
using Business.Models;
using Data.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<IResult> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<Project>> GetAllProjectAsync();
    Task<IResult> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<IResult> UpdateProjectAsync(Expression<Func<ProjectEntity, bool>> expression, Project updatedProject);
    Task<IResult> DeleteProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<IResult> CheckIfExistsAsync(Expression<Func<ProjectEntity, bool>> expression);
}
