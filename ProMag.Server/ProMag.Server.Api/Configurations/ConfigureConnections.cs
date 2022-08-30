using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ProMag.Server.Infrastructure;
using ProMag.Server.Infrastructure.Extensions;

namespace ProMag.Server.Api.Configurations;

public static class ConfigureConnections
{
    public static IServiceCollection AddConnectionProvider(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connection = configuration["ConnectionString"] ?? "";

        services.AddDbContextPool<DataContext>((serviceProvider, optionsBuilder) =>
        {
            optionsBuilder.UseSqlServer(connection);

            optionsBuilder.ReplaceService<IMigrationsAnnotationProvider, CustomAnnotationProvider>();
            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, CustomMigrationsSqlGenerator>();
        });
        
        services.AddSingleton(new SqlConnection(connection));

        return services;
    }
}