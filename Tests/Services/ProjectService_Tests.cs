using Business.Dtos;
using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Tests.Contexts;
using Tests.Helpers;

namespace Tests.Services;

public class ProjectService_Tests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly DataContext _context;
    private readonly IProjectService _projectService;


    public ProjectService_Tests()
    {
        _context = DataContextFactory_Test.Create(); 
        _serviceProvider = TestServiceProvider.Create(_context);
        _projectService = _serviceProvider.GetRequiredService<IProjectService>();
    }

    [Fact]
    public async Task CreateAsync_ShouldAddProjectToDatabase_AndReturnIResult()
    {
        // Arrange
        var form = new ProjectRegistrationForm { ProjectName = "Test", StartDate = "2025-01-01", EndDate = "2025-02-02", CurrentStatus = "Ej Påbörjad", TotalPrice = 15000, 
            Customer = new CustomerRegistrationForm 
            {
                CustomerName = "Test",
            },
            ProjectManager = new EmployeeRegistrationForm
            {
                EmploymentNumber = "1162",
                FirstName = "Test",
                LastName = "Test2",
            },
            Service = new ServiceRegistrationForm
            {
                ServiceName = "Test",
                Price = 1000
            }
        };

        // Act
        var result = await _projectService.CreateProjectAsync(form);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetAllProjectsAsync_ShouldReturnListOfAllProjects()
    {
        // Arrange
        var form = new ProjectRegistrationForm
        {
            ProjectName = "Test",
            StartDate = "2025-01-01",
            EndDate = "2025-02-02",
            CurrentStatus = "Ej Påbörjad",
            TotalPrice = 15000,
            Customer = new CustomerRegistrationForm
            {
                CustomerName = "Test",
            },
            ProjectManager = new EmployeeRegistrationForm
            {
                EmploymentNumber = "1162",
                FirstName = "Test",
                LastName = "Test2",
            },
            Service = new ServiceRegistrationForm
            {
                ServiceName = "Test",
                Price = 1000
            }
        };
        await _projectService.CreateProjectAsync(form);

        // Act
        var result = await _projectService.GetAllProjectAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(form.ProjectName, result.FirstOrDefault(x => x.ProjectName == form.ProjectName)!.ProjectName);
    }

    [Fact]
    public async Task GetProjectAsync_ShouldReturnProject()
    {
        // Arrange
        var form = new ProjectRegistrationForm
        {
            ProjectName = "Test",
            StartDate = "2025-01-01",
            EndDate = "2025-02-02",
            CurrentStatus = "Ej Påbörjad",
            TotalPrice = 15000,
            Customer = new CustomerRegistrationForm
            {
                CustomerName = "Test",
            },
            ProjectManager = new EmployeeRegistrationForm
            {
                EmploymentNumber = "1162",
                FirstName = "Test",
                LastName = "Test2",
            },
            Service = new ServiceRegistrationForm
            {
                ServiceName = "Test",
                Price = 1000
            }
        };
        await _projectService.CreateProjectAsync(form);

        // Act
        var result = await _projectService.GetProjectAsync(x => x.ProjectName == form.ProjectName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.CustomerName);
    }

    [Fact]
    public async Task UpdateProjectAsync_ShouldUpdateProject_AndReturnUpdatedIResult()
    {
        // Arrange
        var form = new ProjectRegistrationForm
        {
            ProjectName = "Test",
            StartDate = "2025-01-01",
            EndDate = "2025-02-02",
            CurrentStatus = "Ej Påbörjad",
            TotalPrice = 15000,
            Customer = new CustomerRegistrationForm
            {
                CustomerName = "Test",
            },
            ProjectManager = new EmployeeRegistrationForm
            {
                EmploymentNumber = "1162",
                FirstName = "Test",
                LastName = "Test2",
            },
            Service = new ServiceRegistrationForm
            {
                ServiceName = "Test",
                Price = 1000
            }
        };
        await _projectService.CreateProjectAsync(form);
        var project = await _projectService.GetProjectAsync(x => x.ProjectName == form.ProjectName);

        // Act
        var result = await _projectService.UpdateProjectAsync(x => x.ProjectName == form.ProjectName, project);
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteProject_ShouldDeleteProjectFromDatabase_AndReturnIResult()
    {
        // Arrange
        var form = new ProjectRegistrationForm
        {
            ProjectName = "Test",
            StartDate = "2025-01-01",
            EndDate = "2025-02-02",
            CurrentStatus = "Ej Påbörjad",
            TotalPrice = 15000,
            Customer = new CustomerRegistrationForm
            {
                CustomerName = "Test",
            },
            ProjectManager = new EmployeeRegistrationForm
            {
                EmploymentNumber = "1162",
                FirstName = "Test",
                LastName = "Test2",
            },
            Service = new ServiceRegistrationForm
            {
                ServiceName = "Test",
                Price = 1000
            }
        };
        await _projectService.CreateProjectAsync(form);
        var project = await _projectService.GetProjectAsync(x => x.ProjectName == form.ProjectName);

        // Act
        var result = await _projectService.DeleteProjectAsync(x => x.Id == project.Id);

        // Assert
        Assert.True(result.Success);
        Assert.Empty(await _projectService.GetAllProjectAsync());
    }

    [Fact]
    public async Task CheckIfExists_ShouldReturnTrue_IfProjectAlreadyExists()
    {
        // Arrange
        var form = new ProjectRegistrationForm
        {
            ProjectName = "Test",
            StartDate = "2025-01-01",
            EndDate = "2025-02-02",
            CurrentStatus = "Ej Påbörjad",
            TotalPrice = 15000,
            Customer = new CustomerRegistrationForm
            {
                CustomerName = "Test",
            },
            ProjectManager = new EmployeeRegistrationForm
            {
                EmploymentNumber = "1162",
                FirstName = "Test",
                LastName = "Test2",
            },
            Service = new ServiceRegistrationForm
            {
                ServiceName = "Test",
                Price = 1000
            }
        };
        await _projectService.CreateProjectAsync(form);
        var project = await _projectService.GetProjectAsync(x => x.ProjectName == form.ProjectName);

        // Act
        var result = await _projectService.CheckIfExistsAsync(x => x.ProjectName == project.CustomerName);

        // Assert
        Assert.True(result.Success);
    }
}
