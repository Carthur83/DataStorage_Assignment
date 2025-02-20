using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IProjectServiceService
    {
        Task<IResult> CreateProjectServiceAsync(ProjectServiceEntity entity);
        Task<IResult> UpdatePojectServiceAsync(Expression<Func<ProjectServiceEntity, bool>> expression, Project updatedProject);
    }
}