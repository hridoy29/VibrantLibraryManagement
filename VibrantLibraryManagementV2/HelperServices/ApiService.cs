
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace VibrantLibraryManagementV2.HelperServices
{
    public class ApiService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public ApiService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public HttpClient CreateAuthenticatedHttpClient()
        {
            var token = _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken").Result;

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return _httpClient;
        }
    }

}
