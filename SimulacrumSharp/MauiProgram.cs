using SimulacrumSharp.Services;
using SimulacrumSharp.Services.Interfaces;
using SimulacrumSharp.ViewModels;

namespace SimulacrumSharp;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		DependencyService.Register<ISimulacrumApiService, SimulacrumApiService>();

		return builder.Build();
	}

	public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<SimulationViewModel>();

        mauiAppBuilder.Services.AddScoped<ISimulacrumApiService, SimulacrumApiService>();

		return mauiAppBuilder;
    }

	public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
		//mauiAppBuilder.Services.AddSingleton<SimulationViewModel>();

		return mauiAppBuilder;
    }
}
