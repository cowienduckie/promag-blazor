using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Server.Library.Constants;
using ProMag.Server.Library.DataTypes;

namespace ProMag.Server.Core.Domain.Supervisor;

public class Supervisor : ISupervisor
{
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;

    public Supervisor(
        IMemoryCache cache,
        IMapper mapper,
        IServiceProvider serviceProvider)
    {
        _cache = cache;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
    }

    private IBaseRepository<TEntity> RepositoryOf<TEntity>() where TEntity : BaseEntity
    {
        return _serviceProvider
            .GetService(typeof(IBaseRepository<TEntity>)) as IBaseRepository<TEntity>
               ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<TReadDto>> GetAllAsync<TEntity, TReadDto>() where TEntity : BaseEntity
    {
        var repository = RepositoryOf<TEntity>();
        var entities = await repository.GetAllAsync();

        SetCache(entities);

        return _mapper.Map<IEnumerable<TReadDto>>(entities);
    }

    public async Task<TReadDto> GetByIdAsync<TEntity, TReadDto>(int id) where TEntity : BaseEntity
    {
        var repository = RepositoryOf<TEntity>();
        var entity = await repository.GetByIdAsync(id);

        SetCache(entity);

        return _mapper.Map<TReadDto>(entity);
    }

    public TReadDto Create<TEntity, TReadDto>(TEntity entity) where TEntity : BaseEntity
    {
        throw new NotImplementedException();
    }

    public bool Update<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        throw new NotImplementedException();
    }

    public bool Delete<TEntity>(int id) where TEntity : BaseEntity
    {
        var repository = RepositoryOf<TEntity>();

        return repository.Delete(id);
    }

    public async Task<bool> SaveAsync<TEntity>() where TEntity : BaseEntity
    {
        var repository = RepositoryOf<TEntity>();

        return await repository.SaveAsync();
    }

    private void SetCache<TEntity>(TEntity entity, MemoryCacheEntryOptions? options = null) where TEntity : BaseEntity
    {
        var cacheEntryOptions = options ?? GetDefaultCacheEntryOptions();

        _cache.Set(GetCacheKey<TEntity>(entity.Id), entity, cacheEntryOptions);
    }

    private void SetCache<TEntity>(IEnumerable<TEntity> entities, MemoryCacheEntryOptions? options = null) where TEntity : BaseEntity
    {
        var cacheEntryOptions = options ?? GetDefaultCacheEntryOptions();

        foreach (var entity in entities)
        {
            _cache.Set(GetCacheKey<TEntity>(entity.Id), entity, cacheEntryOptions);
        }
    }

    private TEntity GetCache<TEntity>(int id)
    {
        return _cache.Get<TEntity>(GetCacheKey<TEntity>(id));
    }

    private void RemoveCache<TEntity>(int id)
    {
        _cache.Remove(GetCacheKey<TEntity>(id));
    }

    private static string GetCacheKey<TEntity>(int id)
    {
        return string.Concat(typeof(TEntity).FullName, "_", id);
    }

    private static MemoryCacheEntryOptions GetDefaultCacheEntryOptions()
    {
        var defaultOptions = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromSeconds(SystemConstants.CacheLifetimeSeconds)
        };

        return defaultOptions;
    }

    public PagedList<TEntity> GetPagedList<TEntity>(IList<TEntity> items, int pageIndex, int pageSize)
    {
        var pagedList = new PagedList<TEntity>(_mapper.Map<IList<TEntity>>(items), pageIndex, pageSize);

        return pagedList;
    }
}