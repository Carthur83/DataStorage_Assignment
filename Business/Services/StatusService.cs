using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusEntity> GetStatusAsync(string currentStatus)
    {
        if (currentStatus == null)
            return null!;

        var statusEntity = await _statusRepository.GetAsync(x => x.StatusType == currentStatus);
        return statusEntity;
    }

    public async Task<IEnumerable<StatusEntity>> GetAllStatusesAsync()
    {
        var statuses = await _statusRepository.GetAllAsync();

        return statuses;
    }
}
