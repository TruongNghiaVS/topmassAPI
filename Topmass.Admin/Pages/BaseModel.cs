using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Topmass.Admin.Pages.Model;
using TopMass.Core.Result;

namespace Topmass.Admin.Pages
{
    public class BaseModel : PageModel
    {

        public ActionResult ReturnResultAPI(BaseResult result)
        {
            return StatusCode(result.StatusCode, result);

        }


        public UserDataView GetUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userData = new UserDataView();
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var idUser = identity.Claims.FirstOrDefault(o => o.Type == "userId")?.Value;
                var userName = userClaims.FirstOrDefault(o => o.Type == "UserName")?.Value;
                var fullName = userClaims.FirstOrDefault(o => o.Type == "FullName")?.Value;
                if (userData == null)
                {
                    userData = new UserDataView();
                }
                userData.UserName = userName;
                userData.FullName = fullName;
                userData.UserId = int.Parse(idUser);
            }
            return userData;
        }

        public int UserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userData = new UserDataView();
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var idUser = identity.Claims.FirstOrDefault(o => o.Type == "userId")?.Value;
                return int.Parse(idUser);
            }
            return -1;
        }
    }
}
