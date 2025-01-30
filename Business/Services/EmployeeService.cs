using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    private readonly IEmployeeRepository _repository = repository;

    public async Task<EmployeeEntity> CreateAsync(EmployeeEntity entity)
    {
        var employeeEntity = await _repository.GetAsync(x => x.Id == entity.Id);
        if (employeeEntity == null)
        {
            employeeEntity = new EmployeeEntity
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };

            await _repository.CreateAsync(employeeEntity);

        }

        return employeeEntity;
    }
}
