using ProMag.Server.Library.DataTypes;

namespace ProMag.Server.Core.Domain.Supervisor;

public interface ISupervisor
{
    PagedList<TEntity> GetPagedList<TEntity>(IList<TEntity> items, int pageIndex, int pageSize);
}