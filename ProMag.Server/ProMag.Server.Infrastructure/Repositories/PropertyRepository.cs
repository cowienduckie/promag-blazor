using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
{
    public PropertyRepository(DataContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await DataContext.Properties
            .Where(e => !e.IsDelete)
            .Include(e => e.Type)
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<Property?> GetByIdAsync(int id)
    {
        return await DataContext.Properties
            .Where(e => !e.IsDelete && e.Id == id)
            .Include(e => e.Type)
            .FirstOrDefaultAsync(); 
    }
}