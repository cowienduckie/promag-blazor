using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Shared.Constants;
using ProMag.Shared.DataTypes;

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

    public async Task<IEnumerable<TReadDto>> GetAllAsync<TEntity, TReadDto>() where TEntity : BaseEntity
    {
        try
        {
            var repository = RepositoryOf<TEntity>();
            var entities = await repository.GetAllAsync();

            SetCache(entities);

            return _mapper.Map<IEnumerable<TReadDto>>(entities);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<TReadDto?> GetByIdAsync<TEntity, TReadDto>(int id) where TEntity : BaseEntity
    {
        try
        {
            var repository = RepositoryOf<TEntity>();
            var entity = GetCache<TEntity>(id) ?? await repository.GetByIdAsync(id);

            if (entity == null) return default;

            SetCache(entity);

            return _mapper.Map<TReadDto>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<TReadDto> CreateAsync<TEntity, TCreateDto, TReadDto>(TCreateDto createDto)
        where TEntity : BaseEntity
    {
        try
        {
            var repository = RepositoryOf<TEntity>();
            var entity = _mapper.Map<TEntity>(createDto);

            repository.Create(entity);

            var result = await repository.SaveAsync();
            if (!result) throw new InvalidOperationException();

            SetCache(entity);
            return _mapper.Map<TReadDto>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync<TEntity, TUpdateDto>(int id, TUpdateDto updateDto) where TEntity : BaseEntity
    {
        try
        {
            var repository = RepositoryOf<TEntity>();
            var entity = _mapper.Map<TEntity>(updateDto);
            entity.Id = id;

            if (!repository.Update(entity) || !await repository.SaveAsync()) return false;

            RemoveCache<TEntity>(id);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : BaseEntity
    {
        try
        {
            var repository = RepositoryOf<TEntity>();

            if (!repository.Delete(id) || !await repository.SaveAsync()) return false;

            RemoveCache<TEntity>(id);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public PagedList<TEntity> GetPagedList<TEntity>(IList<TEntity> items, int pageIndex, int pageSize)
    {
        var pagedList = new PagedList<TEntity>(_mapper.Map<IList<TEntity>>(items), pageIndex, pageSize);

        return pagedList;
    }

    private IBaseRepository<TEntity> RepositoryOf<TEntity>() where TEntity : BaseEntity
    {
        var repo = _serviceProvider
            .GetService(typeof(IBaseRepository<TEntity>));

        return repo as IBaseRepository<TEntity> ?? throw new InvalidOperationException();
    }

    #region Caching Methods

    private void SetCache<TEntity>(TEntity entity, MemoryCacheEntryOptions? options = null) where TEntity : BaseEntity
    {
        var cacheEntryOptions = options ?? GetDefaultCacheEntryOptions();

        _cache.Set(GetCacheKey<TEntity>(entity.Id), entity, cacheEntryOptions);
    }

    private void SetCache<TEntity>(IEnumerable<TEntity> entities, MemoryCacheEntryOptions? options = null)
        where TEntity : BaseEntity
    {
        var cacheEntryOptions = options ?? GetDefaultCacheEntryOptions();

        foreach (var entity in entities) _cache.Set(GetCacheKey<TEntity>(entity.Id), entity, cacheEntryOptions);
    }

    private TEntity? GetCache<TEntity>(int id)
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

    #endregion
}