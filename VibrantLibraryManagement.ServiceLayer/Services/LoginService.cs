using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VibrantLibraryManagement.Core.Models;

namespace VibrantLibraryManagement.ServiceLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jSRuntime;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly string clientId;

        public LoginService(HttpClient httpClient, IConfiguration configuration, IJSRuntime jSRuntime,IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _jSRuntime = jSRuntime;
            _serviceProvider = serviceProvider;
            clientId = configuration["Api:ClientId"];
        }

        public async Task<bool> Login(LoginEntity loginEntity)
        {
            try
            {
                var loginUrl = "/Auth/user/login?clientId=" + clientId;
                var res = await _httpClient.PostAsJsonAsync(loginUrl, loginEntity);

                var loginResponse = await res.Content.ReadFromJsonAsync<LoginResponse>();
                StoreToken(loginResponse.AccessToken);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void StoreToken(string token)
        { // Example using local storage in Blazor
           var jsRuntime = (IJSRuntime)_serviceProvider.GetService(typeof(IJSRuntime)); 
            jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);

        }
}
