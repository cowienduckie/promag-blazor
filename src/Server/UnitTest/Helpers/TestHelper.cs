using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Server.Infrastructure;

namespace ProMag.Server.UnitTest.Helpers;

public class TestHelper
{
    private readonly IServiceProvider _serviceProvider;

    public TestHelper()
    {
        var builder = new DbContextOptionsBuilder<DataContext>();
        builder.UseInMemoryDatabase("ProMagDBInMemory");
        builder.EnableSensitiveDataLogging();

        var dbContextOptions = builder.Options;
        Context = new DataContext(dbContextOptions);
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();

        var server = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                //Test services
            });
        _serviceProvider = server.Services;
    }

    public DataContext Context { get; }

    public IBaseRepository<TEntity> BaseRepositoryInMemory<TEntity>() where TEntity : BaseEntity
    {
        using var scope = _serviceProvider.CreateScope();
        var repoType = scope.ServiceProvider.GetService(typeof(IBaseRepository<TEntity>))!.GetType();

        return Activator.CreateInstance(repoType, Context) as IBaseRepository<TEntity> ??
               throw new InvalidOperationException();
    }
}