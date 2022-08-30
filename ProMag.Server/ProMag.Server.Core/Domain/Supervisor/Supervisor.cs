using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProMag.Server.Library.Constants;
using ProMag.Server.Library.DataTypes;

namespace ProMag.Server.Core.Domain.Supervisor;

public partial class Supervisor : ISupervisor
{
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;

    public Supervisor(
        IMemoryCache cache,
        IMapper mapper)
    {
        _cache = cache;
        _mapper = mapper;
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
