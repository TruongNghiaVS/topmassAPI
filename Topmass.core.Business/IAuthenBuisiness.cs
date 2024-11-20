namespace Topmass.core.Business
{
    public partial interface IAuthenBuisiness
    {
        public Task<LoginResult> LoginCandidate(string username, string password);
        public Task<BaseResult> HandleRequestPassword(string email);
        public Task<LoginResult> ConfirmHasValidResetPasswordLink(string code);
        public Task<BaseResult> ChangePassword(string password, int userId);

    }
}
