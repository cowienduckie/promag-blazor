using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.Domain.Repositories;

public interface IMainTaskRepository : IBaseRepository<MainTask>
{
    Task<IEnumerable<MainTask>> GetByProjectId(int projectId);
}