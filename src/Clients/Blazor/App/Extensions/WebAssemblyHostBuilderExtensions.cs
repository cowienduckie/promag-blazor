using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProMag.Client.Blazor.Infrastructure.Services;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

namespace ProMag.Client.Blazor.App.Extensions;
public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        return builder;
    }

    public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services
            .AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.Configuration["API_BASE_URL"]!)})
            .AddServices();

        return builder;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();

        return services;
    }
}
