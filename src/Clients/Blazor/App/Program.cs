using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProMag.Client.Blazor.App;
using ProMag.Client.Blazor.App.Configurations;

Console.WriteLine("ProMag has started...");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.ConfigureHttpClient();
builder.ConfigureServices();

await builder.Build().RunAsync();