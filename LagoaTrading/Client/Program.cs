using Blazored.LocalStorage;
using LagoaTrading.Client;
using LagoaTrading.Client.Core.Securities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, LagoaTradingAuthStateProvider>();
builder.Services.AddScoped<AbstractLagoaTradingAuthStateProvider, LagoaTradingAuthStateProvider>();

await builder.Build().RunAsync();
