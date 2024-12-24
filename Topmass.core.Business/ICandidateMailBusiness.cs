namespace Topmass.core.Business
{
    public partial interface ICandidateMailBusiness
    {

        public Task<BaseResult> RequestMailValidAccount(string email);
        public Task<BaseResult> HandleRequestPassword(string email);

    }
}
