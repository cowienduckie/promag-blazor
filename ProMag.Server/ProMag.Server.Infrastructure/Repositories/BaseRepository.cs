using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using System.Linq.Expressions;

namespace ProMag.Server.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected DataContext DataContext { get; set; }

    public BaseRepository(DataContext dataContext)
    {
        this.DataContext = dataContext;
    }

    public void Dispose()
    {
        this.DataContext.Dispose();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await this.DataContext
            .Set<TEntity>()
            .Where(e => !e.IsDelete)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await this.DataContext
            .Set<TEntity>()
            .Where(e => !e.IsDelete)
            .Where(expression)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await this.DataContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(e => !e.IsDelete && e.Id == id) ?? throw new InvalidOperationException();
    }

    public TEntity Create(TEntity entity)
    {
        this.DataContext.Add(entity);

        return entity;
    }

    public bool Update(TEntity entity)
    {
        if (!IsExist(entity.Id)) return false;

        entity.LastModified = DateTime.UtcNow;
        this.DataContext.Update(entity);

        return true;
    }

    public bool Delete(int id)
    {
        if (!IsExist(id)) return false;

        var entity = this.DataContext.Set<TEntity>()
            .First(e => e.Id == id);

        entity.LastModified = DateTime.UtcNow;
        entity.IsDelete = true;

        return true;
    }

    public bool IsExist(int id)
    {
        return this.DataContext.Set<TEntity>().Any(e => !e.IsDelete && e.Id == id);
    }

    public async Task<bool> SaveAsync()
    {
        return await this.DataContext.SaveChangesAsync() > 0;
    }
}