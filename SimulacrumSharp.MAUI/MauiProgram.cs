using SimulacrumSharp.MAUI.Data;
using SimulacrumSharp.MAUI.Services;
using SimulacrumSharp.MAUI.Services.Interfaces;

namespace SimulacrumSharp.MAUI
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
            builder.Services.AddBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            builder.RegisterServices();

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddScoped<ISimulacrumApiService, SimulacrumApiService>();

            return mauiAppBuilder;
        }
    }
}