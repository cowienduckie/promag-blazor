using System.Linq.Expressions;

namespace ProMag.Server.Core.Domain.Repositories;

public interface IBaseRepository<TEntity> : IDisposable
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);

    Task<TEntity?> GetByIdAsync(int id);

    TEntity Create(TEntity entity);

    bool Update(TEntity entity);

    bool Delete(int id);

    bool IsExist(int id);

    Task<bool> SaveAsync();
}