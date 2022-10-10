using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProMag.Client.Blazor.App.Extensions;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;

namespace ProMag.Client.Blazor.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("ProMag has started...");

        var builder = WebAssemblyHostBuilder
            .CreateDefault(args)
            .AddRootComponents()
            .AddClientServices();

        var host = builder.Build();

        var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
        await authenticationService.Initialize();

        await host.RunAsync();
    }
}