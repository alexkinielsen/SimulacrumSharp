using SimulacrumSharp.Backend.Helpers;
using SimulacrumSharp.Backend.Helpers.Interfaces;
using SimulacrumSharp.Backend.Models.BigBrother;
using SimulacrumSharp.Backend.Models.DragRace;
using SimulacrumSharp.Backend.Models.Survivor;
using SimulacrumSharp.Backend.Services.Interfaces;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation.Survivor;
using SimulacrumSharp.Backend.Services.Simulation.BigBrother;
using SimulacrumSharp.Backend.Services.Simulation.DragRace;
using SimulacrumSharp.Backend.Services.Simulation.Survivor;
using SimulacrumSharp.Razor.Helpers;
using SimulacrumSharp.Razor.Helpers.Interfaces;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.RegisterServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }

    private static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommonHelper, CommonHelper>();
        builder.Services.AddScoped<ISimulationProvider, SimulationProvider>();

        builder.Services.AddScoped<ISimulationService<SurvivorSeason>, SurvivorSimulationService>();
        builder.Services.AddScoped<ITribalCouncilService, TribalCouncilService>();

        builder.Services.AddScoped<ISimulationService<DragRaceSeason>, DragRaceSimulationService>();

        builder.Services.AddScoped<ISimulationService<BigBrotherSeason>, BigBrotherSimulationService>();
    }
}
