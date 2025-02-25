﻿using Business.Interfaces;
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
            currentStatus = "Ej Påbörjad";

        var statusEntity = await _statusRepository.GetAsync(x => x.StatusType == currentStatus);
        return statusEntity!;
    }

    public async Task<IEnumerable<StatusEntity>> GetAllStatusesAsync()
    {
        var statuses = await _statusRepository.GetAllAsync();

        return statuses;
    }

    public async Task<int> GetStatusIdAsync(string currentStatus)
    {
        if (currentStatus == null)
            return 1;

        var statusEntity = await _statusRepository.GetAsync(x => x.StatusType == currentStatus);
        return statusEntity!.Id;
    }
}
