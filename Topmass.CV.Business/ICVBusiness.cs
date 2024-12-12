using Topmass.CV.Business.Model;

namespace Topmass.CV.Business
{
    public interface ICVBusiness
    {
        public Task<CVReponseAdd> CreateCV(CVRequestAdd request);
        public Task<ApplyJobResponeAdd> ApplyJob(ApplyJobRequestAdd request);
        public Task<GetAllOfHumanRequestReponse> GetAllCVApply(GetAllOfHumanRequest request);

        public Task<GetInfoCVReponse> GetInfo(GetInfoCVRequest request);

        public Task<GetAllCVOfJobReponse> GetAllCVOfJob(GetAllCVOfJobRequest request);

        public Task<ApplyJobWithCreateResponeAdd> ApplyJobWithCV(ApplyJobWithCreateCVAdd request);
        public Task<SearchCVReponse> SearchCV(SearchCVRequestInfo request);
        public Task<SearchCVReponse> GetDetailSearch(string searchId);



        public Task<CVReponseDigitalAdd> AddOrUpdateCVDigital(CVRequestDigitalAdd request);


        public Task<CheckGenFileDigitalReponse> CheckGenFileDigital(int userId);
        public Task<GetAllCVOfJobReponse> GetAllCVApplyNew(FilterGetAllCVApply request);

        public Task<CVReponseDigitalAdd> AddCVToStore(CVRequesAddToStore request);

    }
}
