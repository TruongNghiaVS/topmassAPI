using Topmass.Admin.Business.Model;
using Topmass.Admin.Repository;

namespace Topmass.Admin.Business
{
    public interface IJobAdminBusiness : IBaseBusiness
    {
        public Task<SearchJobAdminReponse> GetAll(SearchJobAdminRequest request);
        public Task<JobAdminDetail> GetDetail(int id);
        public Task<List<JobLogAdminItemDisplay>> GetAllLog(int id);
        public Task<bool> UpdateConfirmStatus(UpdateJobAdmin request);
        public Task<bool> UpdateStatusDisplay(int id, int statusChange, string noted = "", string content = "");
        //public Task<BaseResultAdd> AddNTD(NTDRequestAdd request);

        //job
        public Task<bool> UpdateInfoJob(UpdateJobInfoAdmin requestUpdateJob);

    }
}
