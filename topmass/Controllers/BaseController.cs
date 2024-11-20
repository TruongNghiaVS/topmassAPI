using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using topmass.Model;

namespace topmass.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {

        private readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;


        }
        protected async Task<AuthenInfo> GetCurrentUser()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var idUser = identity.Claims.FirstOrDefault(o => o.Type == "userId")?.Value;
                var userName = userClaims.FirstOrDefault(o => o.Type == "userName")?.Value;
                var firstName = userClaims.FirstOrDefault(o => o.Type == "firstName")?.Value;
                var lastName = userClaims.FirstOrDefault(o => o.Type == "fullName")?.Value;
                var fullName = firstName + " " + lastName;
                return new AuthenInfo
                {
                    UserName = userName,
                    FullName = fullName,
                    Id = idUser
                };
            }
            return new AuthenInfo()
            {
                Id = "-1"
            };
        }
    }
}
