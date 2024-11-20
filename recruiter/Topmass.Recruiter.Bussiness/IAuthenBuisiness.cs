using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{
    public partial interface IAuthenBuisiness
    {
        public Task<LoginResult> Login(string username, string password);
        public Task<BaseResult> HandleRequestPassword(string email);
        public Task<LoginResult> ConfirmHasValidResetPasswordLink(string code);
        public Task<LoginResult> ConfirmToValidAccoutByCode(string code);

        public Task<BaseResult> ChangePassword(string password, int userId);
        public Task<BaseResult> ChangePasswordNotMail(string password, int userId);

    }
}
