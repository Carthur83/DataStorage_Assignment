using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Tests.Contexts;
using Tests.Helpers;

namespace Tests.Services;

public class ProjectServiceService
{
    private readonly DataContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectServiceService _projectServiceService;

    public ProjectServiceService()
    {
        _context = DataContextFactory_Test.Create();
        _serviceProvider = TestServiceProvider.Create(_context);
        _projectServiceService = _serviceProvider.GetRequiredService<IProjectServiceService>();
    }
}
