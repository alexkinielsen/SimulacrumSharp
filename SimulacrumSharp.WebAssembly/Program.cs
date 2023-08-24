using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimulacrumSharp.WebAssembly;
using SimulacrumSharp.Backend.Helpers;
using SimulacrumSharp.Backend.Helpers.Interfaces;
using SimulacrumSharp.Backend.Models.Survivor;
using SimulacrumSharp.Backend.Services.Interfaces;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation.BigBrother;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation.Survivor;
using SimulacrumSharp.Backend.Services.Simulation.BigBrother;
using SimulacrumSharp.Backend.Services.Simulation.DragRace;
using SimulacrumSharp.Backend.Services.Simulation.Survivor;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.RegisterServices();

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        var app = builder.Build();

        await app.RunAsync();
    }

    private static void RegisterServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<ICommonHelper, CommonHelper>();

        builder.Services.AddScoped<ISimulationService<SurvivorSeason>, SurvivorSimulationService>();
        builder.Services.AddScoped<ITribalCouncilService, TribalCouncilService>();

        builder.Services.AddScoped<IDragRaceSimulationService, DragRaceSimulationService>();

        builder.Services.AddScoped<IBigBrotherSimulationService, BigBrotherSimulationService>();
    }
}