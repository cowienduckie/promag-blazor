using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class SubTaskRepository : BaseRepository<SubTask>, ISubTaskRepository
{
    public SubTaskRepository(DataContext context) : base(context)
    {
    }
}