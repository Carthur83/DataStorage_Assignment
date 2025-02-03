using Data.Entities;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<StatusEntity> GetStatusAsync(string currentStatus);
        Task<IEnumerable<StatusEntity>> GetAllStatusesAsync();
    }
}