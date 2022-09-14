using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Server.Infrastructure;
using ProMag.Server.UnitTest.Helpers;

namespace ProMag.Server.UnitTest.Repositories;

public abstract class BaseRepositoryTest<TEntity> : IDisposable where TEntity : BaseEntity
{
    protected readonly DataContext _context;
    protected readonly TestHelper _helper;
    protected readonly IBaseRepository<TEntity>? _repository;

    protected BaseRepositoryTest()
    {
        _helper = new TestHelper();
        _context = _helper.Context;
        _repository = _helper.BaseRepositoryInMemory<TEntity>();
    }

    public void Dispose()
    {
        _repository?.Dispose();
    }
}