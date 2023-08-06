namespace SimulacrumSharp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private void OnSurvivorNavClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            SurvivorNav.Text = $"Clicked {count} time";
        else
            SurvivorNav.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnDragRaceNavClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            DragRaceNav.Text = $"Clicked {count} time";
        else
            DragRaceNav.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

