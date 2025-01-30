using Data.Entities;

namespace Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeEntity> CreateAsync(EmployeeEntity entity);
    }
}