namespace ProMag.Server.Core.Domain.Repositories;

public interface IBaseRepository<TEntity> : IDisposable
{
    IEnumerable<TEntity> GetAll();

    TEntity GetById(int id);

    TEntity Create(TEntity entity);

    bool Update(TEntity entity);

    bool Delete(int id);

    bool IsExist(int id);
}