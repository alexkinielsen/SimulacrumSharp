using SimulacrumSharp.Models;
using SimulacrumSharp.Services.Interfaces;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace SimulacrumSharp.Services
{
    public class SimulacrumApiService : ISimulacrumApiService
    {
        private static string ApiUrl = 
            DeviceInfo.Platform.Equals(DevicePlatform.Android)
                ? "http://10.0.2.2:5250"
                : "https://localhost:7250";

        private readonly HttpClient _client;
        private JsonSerializerOptions _serializerOptions;

        public SimulacrumApiService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<ApiResponse> Post(string controller, string endpoint, object requestObject)
        {
            var uriString = string.Join("/", ApiUrl, controller, endpoint);

            var uri = new Uri(uriString);
            var requestJson = JsonSerializer.Serialize(requestObject, _serializerOptions);
            var requestBody = new StringContent(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            try
            {
                var result = await _client.PostAsync(uri, requestBody);

                var resultString = result.Content?.ReadAsStringAsync()?.Result ?? string.Empty;
                var response = new ApiResponse
                {
                    StatusCode = (int)result.StatusCode,
                    ResponseBody = resultString
                };

                return response;
            }
            catch(Exception ex)
            {
                //TODO: Log
                return null;
            }
        }
    }
}
