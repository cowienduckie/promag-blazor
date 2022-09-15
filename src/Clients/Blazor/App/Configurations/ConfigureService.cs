
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProMag.Clients.Blazor.Infrastructure.Services;
using ProMag.Clients.Blazor.Infrastructure.Services.Interfaces;

namespace ProMag.Client.Blazor.App.Configurations;
public static class ConfigureService
{
    public static void ConfigureServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient<IProjectService, ProjectService>
                (spds => spds.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
    }

    public static void ConfigureHttpClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
    }
}