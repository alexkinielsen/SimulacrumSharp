using SimulacrumSharp.Backend.Helpers;
using SimulacrumSharp.Backend.Helpers.Interfaces;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation.BigBrother;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation.Survivor;
using SimulacrumSharp.Backend.Services.Simulation.BigBrother;
using SimulacrumSharp.Backend.Services.Simulation.DragRace;
using SimulacrumSharp.Backend.Services.Simulation.Survivor;
using SimulacrumSharp.Web.Data;

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

        builder.Services.AddScoped<ISurvivorSimulationService, SurvivorSimulationService>();
        builder.Services.AddScoped<ITribalCouncilService, TribalCouncilService>();

        builder.Services.AddScoped<IDragRaceSimulationService, DragRaceSimulationService>();

        builder.Services.AddScoped<IBigBrotherSimulationService, BigBrotherSimulationService>();
    }
}
