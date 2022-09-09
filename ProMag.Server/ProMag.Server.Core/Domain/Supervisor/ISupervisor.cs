using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Server.Library.DataTypes;

namespace ProMag.Server.Core.Domain.Supervisor;

public interface ISupervisor
{
    PagedList<TEntity> GetPagedList<TEntity>(IList<TEntity> items, int pageIndex, int pageSize);
    Task<IEnumerable<TReadDto>> GetAllAsync<TEntity, TReadDto>() where TEntity : BaseEntity;
    Task<TReadDto> GetByIdAsync<TEntity, TReadDto>(int id) where TEntity : BaseEntity;
    TReadDto Create<TEntity, TReadDto>(TEntity entity) where TEntity : BaseEntity;
    bool Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
    bool Delete<TEntity>(int id) where TEntity : BaseEntity;
    Task<bool> SaveAsync<TEntity>() where TEntity : BaseEntity;
}