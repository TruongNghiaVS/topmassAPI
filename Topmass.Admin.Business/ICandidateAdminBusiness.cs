using Topmass.Admin.Business.Model;
using Topmass.Admin.Repository;

namespace Topmass.Admin.Business
{
    public interface ICandidateAdminBusiness : IBaseBusiness
    {
        public Task<SearchCandidateAdminReponse> GetAll(SearchCandidateAdminRequest request);
        public Task<JobAdminDetail> GetDetail(int id);
        public Task<List<JobLogAdminItemDisplay>> GetAllLog(int id);
        public Task<bool> UpdateConfirmStatus(UpdateJobAdmin request);
        public Task<bool> UpdateStatusDisplay(int id, int statusChange, string noted = "", string content = "");

    }
}
