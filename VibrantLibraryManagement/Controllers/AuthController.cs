using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace VibrantLibraryManagement.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        [HttpGet("login-google")]
        public IActionResult LoginGoogle()
        {
            var authenticationProperties = new AuthenticationProperties { RedirectUri = "/" };
            return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
        }
    }
}
