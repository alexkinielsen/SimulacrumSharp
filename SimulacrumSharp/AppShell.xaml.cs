using SimulacrumSharp.Views;

namespace SimulacrumSharp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(SurvivorIndexPage), typeof(SurvivorIndexPage));
        Routing.RegisterRoute(nameof(DragRaceIndexPage), typeof(SurvivorIndexPage));
    }
}
