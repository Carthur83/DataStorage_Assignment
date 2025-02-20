using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {

        var entities = await _context.Projects
            .Include(customer => customer.Customer)
            .Include(status => status.Status)
            .Include(ps => ps.ProjectService)
            .ThenInclude(x => x.Service)
            .Include(employee => employee.Employee)
            .ToListAsync();

        return entities;
    }
}
