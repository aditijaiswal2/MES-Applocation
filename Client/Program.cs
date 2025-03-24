using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MES.Client;
using MudBlazor.Services;
using Blazored.LocalStorage;
using MES.Shared.Models;
using MES.Client.Utitlity;

namespace MES.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddMudServices();
        builder.Services.AddSingleton<IDialogCompletionService, CompletionService>();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<Receiving>();
        await builder.Build().RunAsync();
    }
}
