namespace Topmass.CV.Business
{
    public interface IProfileCVBusiness
    {

        public Task<dynamic> GetFullProfileUser(string searchId, int userHuamnId);
        public Task<dynamic> GetDetailInfo(string searchId, int userId);
    }
}
