using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Server.Infrastructure;
using ProMag.Server.Infrastructure.Repositories;

namespace ProMag.Server.UnitTest.Helpers;

public class TestHelper
{
    private readonly DataContext _context;
    private readonly IServiceProvider _serviceProvider;
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

        var server = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                //Test services
            });
        _serviceProvider = server.Services;
    }

    public IBaseRepository<TEntity> BaseRepositoryInMemory<TEntity>() where TEntity : BaseEntity
    {
        using var scope = _serviceProvider.CreateScope();
        var repoType = scope.ServiceProvider.GetService(typeof(IBaseRepository<TEntity>))!.GetType();

        return Activator.CreateInstance(repoType, _context) as IBaseRepository<TEntity> ?? throw new InvalidOperationException();
    }
}