using Topmass.Job.Business.Model.webCan;

namespace Topmass.Job.Business
{
    public interface IJobUserBusiness
    {

        public Task<GetAllCVApplyReponse> GetAllCVApply(int userId, int status = -1);

        public Task<GetAllJpobSaveReponse> GetAllJobSave(int userId, int orderBy);

    }
}
