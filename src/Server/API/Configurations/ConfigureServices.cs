using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;
using ProMag.Server.Core.Domain.Supervisor;
using ProMag.Server.Infrastructure;
using ProMag.Server.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProMag.Server.Api.Configurations;

public static class ConfigureServices
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IMainTaskRepository, MainTaskRepository>();
        services.AddScoped<IBaseRepository<Project>, ProjectRepository>();
        services.AddScoped<IBaseRepository<Property>, PropertyRepository>();
        services.AddScoped<IBaseRepository<MainTask>, MainTaskRepository>();
        services.AddScoped<IBaseRepository<SubTask>, SubTaskRepository>();
    }

    public static void ConfigureSupervisor(this IServiceCollection services)
    {
        services.AddScoped<ISupervisor, Supervisor>();
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        //Identity Register
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

        //Identity Options
        services.Configure<IdentityOptions>(options =>
        {
            //Config Password
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 1;

            //Config Lockout
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            //Config User
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            //Config Login
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ProMag API",
                Description = "A Project Management Tool",
                Contact = new OpenApiContact
                {
                    Name = "Minh Tran",
                    Url = new Uri("https://lowkeycode.me")
                }
            });
            //c.OperationFilter<AddRequiredHeaderParameter>();
        });
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    public static void AddNewtonsoft(this IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(s =>
        {
            s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            s.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
    }

    public static void AddCorsMechanism(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }

    public static void AddServerLogging(this IServiceCollection services)
    {
        services.AddLogging(builder => builder
            .AddConsole()
            .AddFilter(level => level >= LogLevel.Information)
        );
    }

    public static void AddCaching(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddResponseCaching();
    }
}

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Required = false
        });
    }
}