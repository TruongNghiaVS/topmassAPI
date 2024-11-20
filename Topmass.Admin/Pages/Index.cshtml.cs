using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Topmass.Admin.Pages.Model;

namespace Topmass.Admin.Pages
{
    public class IndexModel : BaseModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public UserDataView UserData
        {
            get; set;
        }
        public async Task<ActionResult> OnGet()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostLogOut(LoginRequest request)
        {
            var authenticationScheme =
                HttpContext.User
                .FindFirstValue
                (ClaimTypes.AuthenticationMethod);
            if (authenticationScheme == null)
            {

            }
            await HttpContext.SignOutAsync(authenticationScheme);
            return Redirect("/login");
        }
    }
}
