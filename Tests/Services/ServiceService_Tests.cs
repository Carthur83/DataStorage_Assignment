using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Tests.Contexts;
using Tests.Helpers;

namespace Tests.Services;

public class ServiceService_Tests
{
    private readonly DataContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceService _serviceService;

    public ServiceService_Tests()
    {
        _context = DataContextFactory_Test.Create();
        _serviceProvider = TestServiceProvider.Create(_context);
        _serviceService = _serviceProvider.GetRequiredService<IServiceService>();
    }
}
