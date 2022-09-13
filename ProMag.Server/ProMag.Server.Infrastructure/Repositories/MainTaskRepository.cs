using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class MainTaskRepository : BaseRepository<MainTask>, IMainTaskRepository
{
    public MainTaskRepository(DataContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<MainTask>> GetAllAsync()
    {
        return await DataContext.MainTasks
            .Where(e => !e.IsDelete)
            .Include(e => e.Status)
            .Include(e => e.Properties)
            .Include(e => e.SubTasks)
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<MainTask?> GetByIdAsync(int id)
    {
        return await DataContext.MainTasks
            .Where(e => !e.IsDelete && e.Id == id)
            .Include(e => e.Status)
            .Include(e => e.Properties)
            .Include(e => e.SubTasks)
            .FirstOrDefaultAsync(); 
    }
}