using Topmass.CV.Repository.Model;

namespace Topmass.CV.Repository
{
    public interface ICVRepository
    {
        public Task<CVapplyJobReponse> ApplyJob(CVapplyJobRequest request);
        public Task<CVResumeResponse> CreateCV(CVResumeRequest request);
        public Task<GetAllCVReponse> GetAllCVOfCan(CVResumeRequest request);
        public Task<GetAllCVByJobReponse> GetAllCVByJob(GetAllCVByJobRequest request);
        public Task<GetAllCVByCampaignReponse> GetAllCVApply(GetAllCVByCampaignRequest request);

        public Task<ApplyJobWithCreateCVReponse> ApplyJobWithCreateCV(ApplyJobWithCreateCV request);


    }
}
