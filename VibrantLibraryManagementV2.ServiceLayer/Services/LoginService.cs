
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VibrantLibraryManagementV2.Core.Models;

namespace VibrantLibraryManagementV2.ServiceLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jSRuntime;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly string clientId;

        public LoginService(HttpClient httpClient, IConfiguration configuration, IJSRuntime jSRuntime, IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _jSRuntime = jSRuntime;
            _serviceProvider = serviceProvider;
            clientId = configuration["Api:ClientId"];
        }

        public async Task<LoginResponse> Login(LoginEntity loginEntity)
        {
            try
            {
                var loginUrl = "/Auth/user/login?clientId=" + clientId;
                var res = await _httpClient.PostAsJsonAsync(loginUrl, loginEntity);

                var loginResponse = await res.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse.Status==200)
                {
                    return loginResponse;
                }
                return loginResponse;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
