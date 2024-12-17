using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace VibrantLibraryManagementV2.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        [HttpGet("login-google")]
        public IActionResult LoginGoogle()
        {
            var authenticationProperties = new AuthenticationProperties { RedirectUri = "/google-success" };
            return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
        }
    }
}
