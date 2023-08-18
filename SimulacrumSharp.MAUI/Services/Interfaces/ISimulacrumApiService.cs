using SimulacrumSharp.MAUI.Models;

namespace SimulacrumSharp.MAUI.Services.Interfaces
{
    public interface ISimulacrumApiService
    {
        Task<ApiResponse> Post(string controller, string endpoint, object requestObject);
    }
}
