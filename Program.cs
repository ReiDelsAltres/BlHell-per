using BlazorAnimation;
using BlHell_per;
using BlHell_per.Core.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Extensions;
using MudBlazor.Services;
//using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Static.HttpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => Static.HttpClient);
builder.Services.AddScoped<PWAService>();

//builder.Services.AddPWAUpdater();
builder.Services.AddMudServices();
builder.Services.AddMudExtensions(c => c.WithoutAutomaticCssLoading());
builder.Services.Configure<AnimationOptions>(Guid.NewGuid().ToString(), c => { });

await builder.Build().RunAsync();

public class Static {
    public static HttpClient HttpClient { get; set; }
}
