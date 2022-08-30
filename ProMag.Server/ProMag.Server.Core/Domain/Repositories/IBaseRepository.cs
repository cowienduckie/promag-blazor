namespace ProMag.Server.Core.Domain.Repositories;

public interface IBaseRepository<TEntity> : IDisposable
{
    IEnumerable<TEntity> GetAll(string userId);

    TEntity GetById(int id, string userId);

    TEntity Create(TEntity entity, string userId);

    bool Update(TEntity entity, string userId);

    bool Delete(int id, string userId);

    bool IsExist(int id);
}