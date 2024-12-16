using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibrantLibraryManagement.Core.Models;

namespace VibrantLibraryManagement.ServiceLayer.Services
{
    public interface ILoginService
    {
        public Task<LoginResponse> Login(LoginEntity loginEntity);
    }
}
