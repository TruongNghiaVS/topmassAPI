

using Topmass.CV.Business.Model;


namespace Topmass.CV.Business
{
    public interface ICVUserBusiness
    {
        public Task<GetAllCVUserReponse> GetAllCV(int UserId);







    }
}
