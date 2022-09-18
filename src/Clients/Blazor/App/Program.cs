using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProMag.Client.Blazor.App.Extensions;

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

        await builder.Build().RunAsync();
    }
}