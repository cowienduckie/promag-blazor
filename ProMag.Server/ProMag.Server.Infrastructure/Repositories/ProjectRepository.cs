using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(DataContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await DataContext.Projects
            .Where(e => !e.IsDelete)
            .Include(e => e.Status)
            .Include(e => e.MainTasks)
            .Include(e => e.DefaultProperties)
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<Project?> GetByIdAsync(int id)
    {
        return await DataContext.Projects
            .Where(e => !e.IsDelete && e.Id == id)
            .Include(e => e.Status)
            .Include(e => e.MainTasks)
            .Include(e => e.DefaultProperties)
            .FirstOrDefaultAsync();
    }
}