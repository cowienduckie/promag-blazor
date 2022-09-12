using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Server.Infrastructure;
using ProMag.Server.Infrastructure.Repositories;

namespace ProMag.Server.UnitTest.Helpers;

public class TestHelper
{
    private readonly DataContext _context;
    public DataContext Context => _context;

    public TestHelper()
    {
        var builder = new DbContextOptionsBuilder<DataContext>();
        builder.UseInMemoryDatabase(databaseName: "ProMagDBInMemory");
        builder.EnableSensitiveDataLogging();

        var dbContextOptions = builder.Options;
        _context = new DataContext(dbContextOptions);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public IBaseRepository<TEntity> BaseRepositoryInMemory<TEntity>() where TEntity : BaseEntity
    {
        return new BaseRepository<TEntity>(_context);
    }
}