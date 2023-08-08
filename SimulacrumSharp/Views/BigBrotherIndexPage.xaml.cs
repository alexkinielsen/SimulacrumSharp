using SimulacrumSharp.Services;
using SimulacrumSharp.Services.Interfaces;
using SimulacrumSharp.ViewModels;

namespace SimulacrumSharp.Views
{
    public partial class BigBrotherIndexPage : ContentPage
    {
        private readonly ISimulacrumApiService _apiService;

        public BigBrotherIndexPage()
        {
            InitializeComponent();

            _apiService = DependencyService.Get<ISimulacrumApiService>();
        }

        private async void OnSimulate(object sender, EventArgs e)
        {
            var simulationResponse = await _apiService.Post("BigBrotherSimulation", string.Empty, new());

            if (simulationResponse != null)
            {
                SimulateResults.Text = simulationResponse.ResponseBody;
            }
            else
            {
                SimulateResults.Text = "An unknown error occurred.";
            }
            
        }
    }
}
