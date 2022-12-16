using Microsoft.AspNetCore.Mvc;

namespace oldprogrammer_authentication.Controllers
{
    public class RegisterController : ControllerBase
    {
        [HttpGet]
        public string Ping()
        {
            return "Register Controller is working";
        }
    }
}
