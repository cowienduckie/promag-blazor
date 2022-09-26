using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProMag.Server.Infrastructure;

namespace ProMag.Server.Api.Configurations;

public static class ConfigureConnections
{
    public static IServiceCollection AddConnectionProvider(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connection = configuration["LocalConnectionString"] ?? configuration["ConnectionString"];

        services.AddDbContextPool<DataContext>(optionsBuilder => optionsBuilder.UseSqlServer(connection));

        services.AddSingleton(new SqlConnection(connection));

        return services;
    }
}