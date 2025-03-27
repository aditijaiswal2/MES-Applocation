using Blazored.LocalStorage;
using MES.Client;
using MES.Client.Utitlity;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("MES.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MES.ServerAPI"));

builder.Services.AddSingleton<IDialogCompletionService, CompletionService>();

builder.Services.AddBlazoredLocalStorage();

#region UI Service
builder.Services.AddMudServices();
#endregion UI Service

await builder.Build().RunAsync();
