using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectServiceService(IProjectServiceRepository repository) : IProjectServiceService
{
    private readonly IProjectServiceRepository _repository = repository;

    public async Task<IResult> CreateProjectServiceAsync(ProjectServiceEntity entity)
    {
        try
        {
            await _repository.CreateAsync(entity);
            await _repository.SaveAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);

            return Result.Error("Nåt gick fel, försök igen");
        }
    }

    public async Task<IResult> UpdatePojectServiceAsync(Expression<Func<ProjectServiceEntity, bool>> expression, Project updatedProject)
    {
        if (updatedProject == null)
            return null!;

        try
        {
            var existingEntity = await _repository.GetAsync(expression);
            if (existingEntity == null)
                return Result.NotFound("Nåt gick fel");

            existingEntity.ServiceId = updatedProject.ServiceId;
            existingEntity.Price = updatedProject.Price;

            _repository.Update(existingEntity);
            await _repository.SaveAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Error("Nåt gick fel, ej uppdaterat");
        }
    }
}
