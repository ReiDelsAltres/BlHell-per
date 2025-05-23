using BlazorDownloadFile;
using BlHell_per;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Static.HttpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => Static.HttpClient);
builder.Services.AddBlazorDownloadFile(ServiceLifetime.Scoped);
builder.Services.AddPWAUpdater();
builder.Services.AddMudServices();

await builder.Build().RunAsync();

public class Static {
    public static HttpClient HttpClient { get; set; }
}
