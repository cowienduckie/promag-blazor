using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected BaseRepository(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    protected DataContext DataContext { get; }

    public void Dispose()
    {
        DataContext.Dispose();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DataContext
            .Set<TEntity>()
            .Where(e => !e.IsDelete)
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await DataContext
            .Set<TEntity>()
            .Where(e => !e.IsDelete)
            .Where(expression)
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await DataContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(e => !e.IsDelete && e.Id == id);
    }

    public TEntity Create(TEntity entity)
    {
        DataContext.Add(entity);

        return entity;
    }

    public bool Update(TEntity entity)
    {
        if (!IsExist(entity.Id)) return false;

        entity.LastModified = DateTime.UtcNow;
        DataContext.Update(entity);

        return true;
    }

    public bool Delete(int id)
    {
        if (!IsExist(id)) return false;

        var entity = DataContext.Set<TEntity>()
            .First(e => e.Id == id);

        entity.LastModified = DateTime.UtcNow;
        entity.IsDelete = true;

        return true;
    }

    public bool IsExist(int id)
    {
        return DataContext.Set<TEntity>().Any(e => !e.IsDelete && e.Id == id);
    }

    public async Task<bool> SaveAsync()
    {
        return await DataContext.SaveChangesAsync() > 0;
    }
}