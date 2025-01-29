using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ProjectManagerRepository(DataContext context) : BaseRepository<ProjectManagerEntity>(context), IProjectManagerRepository
{
    private readonly DataContext _context = context;
}
