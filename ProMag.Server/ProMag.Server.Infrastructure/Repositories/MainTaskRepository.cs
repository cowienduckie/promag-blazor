using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class MainTaskRepository : BaseRepository<MainTask>, IMainTaskRepository
{
    public MainTaskRepository(DataContext context) : base(context)
    {
    }
}