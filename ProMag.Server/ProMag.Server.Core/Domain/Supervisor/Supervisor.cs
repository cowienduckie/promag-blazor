using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Server.Library.Constants;
using ProMag.Server.Library.DataTypes;

namespace ProMag.Server.Core.Domain.Supervisor;

public partial class Supervisor : ISupervisor
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
        var entityList =  await repository.GetAllAsync();

        return _mapper.Map<IEnumerable<TReadDto>>(entityList);
    }

    public async Task<TReadDto> GetByIdAsync<TEntity, TReadDto>(int id) where TEntity : BaseEntity
    {
        var repository = RepositoryOf<TEntity>();

        return _mapper.Map<TReadDto>(await repository.GetByIdAsync(id));
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

    #region Shared Methods

    private void SetCache<TEntity>(int id, TEntity value, string userId)
    {
        var cacheEntryOptions =
            new MemoryCacheEntryOptions().SetSlidingExpiration(
                TimeSpan.FromSeconds(SystemConstants.CacheLifetimeSeconds));
        var key = string.Concat(typeof(TEntity).FullName, "-", id) + "-" + userId;
        _cache.Set(key, value, cacheEntryOptions);
    }

    private TEntity GetCache<TEntity>(int id, string userId)
    {
        var key = string.Concat(typeof(TEntity).FullName, "-", id) + "-" + userId;

        return _cache.Get<TEntity>(key);
    }

    public PagedList<TEntity> GetPagedList<TEntity>(IList<TEntity> items, int pageIndex, int pageSize)
    {
        var pagedList = new PagedList<TEntity>(_mapper.Map<IList<TEntity>>(items), pageIndex, pageSize);

        return pagedList;
    }

    #endregion Shared Methods
}
