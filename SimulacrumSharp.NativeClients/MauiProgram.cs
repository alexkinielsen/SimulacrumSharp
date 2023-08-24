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
            builder.Services.AddScoped<ISimulationProvider, SimulationProvider>();

            builder.Services.AddScoped<ISimulationService<SurvivorSeason>, SurvivorSimulationService>();
            builder.Services.AddScoped<ITribalCouncilService, TribalCouncilService>();

            builder.Services.AddScoped<ISimulationService<DragRaceSeason>, DragRaceSimulationService>();

            builder.Services.AddScoped<ISimulationService<BigBrotherSeason>, BigBrotherSimulationService>();
        }
    }
}