using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly string clientId;

        public LoginService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            clientId = configuration["Api:ClientId"];
        }

        public async Task<bool> Login(LoginEntity loginEntity)
        {
            try
            {
                var loginUrl = "/Auth/user/login?clientId=" + clientId;
                var res = await _httpClient.PostAsJsonAsync(loginUrl, loginEntity);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
