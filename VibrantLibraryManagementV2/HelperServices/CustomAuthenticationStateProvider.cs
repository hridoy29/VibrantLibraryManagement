using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace VibrantLibraryManagementV2.HelperServices
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISessionStorageService _sessionStorageService;
        private AuthenticationState _anonymousState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        private AuthenticationState _authenticatedState;
        private bool _isInitialized;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IHttpContextAccessor httpContextAccessor)
        {
            _jsRuntime = jsRuntime;
            _httpContextAccessor = httpContextAccessor;
            //_sessionStorageService = sessionStorageService;
            _anonymousState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_isInitialized)
            {
                return _authenticatedState ?? _anonymousState;
            }

            _ = InitializeAuthenticationStateAsync();
            return _anonymousState;
        }

        public async Task InitializeAuthenticationStateAsync()
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

                if (!string.IsNullOrEmpty(token))
                {
                    var claims = JwtParser.ParseClaimsFromJwt(token);
                    var identity = new ClaimsIdentity(claims, "jwt");
                    var userPrincipal = new ClaimsPrincipal(identity);

                    _authenticatedState = new AuthenticationState(userPrincipal);
                }
                else if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
                {
                    var userPrincipal = _httpContextAccessor.HttpContext.User;
                    _authenticatedState = new AuthenticationState(userPrincipal);
                }
                else
                {
                    _authenticatedState = _anonymousState;
                }

                _isInitialized = true;
                NotifyAuthenticationStateChanged(Task.FromResult(_authenticatedState));
            }
            catch (Exception ex)
            {
                _authenticatedState = _anonymousState;
            }
        }


        public async Task SetAuthenticationStateAsync(string token = null)
        {
            ClaimsIdentity identity;

            if (!string.IsNullOrEmpty(token))
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);

                var claims = JwtParser.ParseClaimsFromJwt(token);
                identity = new ClaimsIdentity(claims, "jwt");
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
                identity = new ClaimsIdentity();
            }

            var userPrincipal = new ClaimsPrincipal(identity);
            var authState = new AuthenticationState(userPrincipal);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task LogoutAsync()
        {
            await SetAuthenticationStateAsync(null);
        }
    }
}
