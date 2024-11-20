using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Topmass.Admin.Business;
using Topmass.Admin.Pages.Model;
using TopMass.Core.Result;


namespace Topmass.Admin.Pages
{
    public class LoginModel : BaseModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IloginBusiness bussiness;
        public LoginModel(ILogger<LoginModel> logger, IloginBusiness _bussiness
            )
        {
            _logger = logger;
            bussiness = _bussiness;
        }
        private async Task<IActionResult> Login(string userName, string pass)
        {
            var userProfile = await bussiness.Login(userName, pass);
            if (userProfile == null || userProfile.Id < 1)
            {
                var listError = new
                {
                    name = "UserName",
                    Content = "Tên đăng nhập hoặc mật khẩu không chính xác"
                };
                ModelState.AddModelError("UserName", "Tên đăng nhập hoặc mật khẩu không chính xác");
                return Page();

            }

            var account = new
            {
                id = userProfile.Id,
                userProfile.UserName,
                userProfile.FullName
            };
            List<Claim> claims = new List<Claim>
            {
                new Claim("userId", account.id.ToString()),
                new Claim("UserName", account.UserName),

            };
            if (!string.IsNullOrWhiteSpace(account.FullName))
                claims.Add(new Claim("FullName", account.FullName));

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false
            };
            await HttpContext.SignInAsync(principal);
            return Redirect("/");
        }
        public void OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                HttpContext.Response.Redirect("/");
            }
        }
        public async Task<IActionResult> OnPostLogin(LoginRequest request)
        {
            var result = new BaseResult();
            if (string.IsNullOrEmpty(request.UserName))
            {
                result.AddError(nameof(request.UserName), "Thiếu thông tin tên đăng nhập");
                return ReturnResultAPI(result);

            }
            return await Login(request.UserName, request.Password);

        }
    }

}
