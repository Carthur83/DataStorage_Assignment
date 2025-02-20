using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Tests.Contexts;
using Tests.Helpers;

namespace Tests.Services;

public class StatusService_Tests
{
    private readonly DataContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly IStatusService _statusService;

    public StatusService_Tests()
    {
        _context = DataContextFactory_Test.Create();
        _serviceProvider = TestServiceProvider.Create(_context);
        _statusService = _serviceProvider.GetRequiredService<IStatusService>();
    }
}
