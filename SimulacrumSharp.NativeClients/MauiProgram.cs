using Microsoft.AspNetCore.Components.WebView.Maui;
using SimulacrumSharp.NativeClients.Data;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation.Survivor;
using SimulacrumSharp.Backend.Services.Simulation.Survivor;
using SimulacrumSharp.Backend.Helpers.Interfaces;
using SimulacrumSharp.Backend.Helpers;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation;
using SimulacrumSharp.Backend.Services.Simulation.DragRace;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation.BigBrother;
using SimulacrumSharp.Backend.Services.Simulation.BigBrother;

namespace SimulacrumSharp.NativeClients
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            builder.RegisterServices();

            return builder.Build();
        }

        private static void RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<ICommonHelper, CommonHelper>();

            builder.Services.AddScoped<ISurvivorSimulationService, SurvivorSimulationService>();
            builder.Services.AddScoped<ITribalCouncilService, TribalCouncilService>();

            builder.Services.AddScoped<IDragRaceSimulationService, DragRaceSimulationService>();

            builder.Services.AddScoped<IBigBrotherSimulationService, BigBrotherSimulationService>();
        }
    }
}