
using Topmass.Job.Business.Model;
using TopMass.Core.Result;

namespace Topmass.Job.Business
{
    public interface IJobBusiness
    {

        public Task<JobItemReponse> AddJob(JobItemBusinessAdd request);

        public Task<JobItemReponse> UpdateJob(JobItemBusinessUpdate request);
        public Task<DataResult> UpdateJob(JobItemUpdate request);

        public Task<BaseResult> GetInfo(JobInfoRequest request);

        public Task<BaseResult> ChangeStatus(JobItemStatusUpdate request);

        public Task<BaseResult> GetallJob(JobSearchRequest request);




        public Task<BaseResult> GetAllViewerOfJob(GetAllVierOfJobRequest request);

        public Task<BaseResult> AddViewJob(ViewJobUserAddRequest request);
        public Task<ReportStaticInfoOverviewItem> GetOverviewJob(GetOverViewByJobId request);
        public Task<GetInfoForEditReponse> GetInfoForEdit(JobInfoRequest jobInfo);

        // candidate:
        public Task<JobDetailResult> GetInfoJOb(CandidateJobInfoRequest request);
        public Task<dynamic> GetDetailMetadata(string jobSlug);
        public Task<JobRelattionReponse> GetRelationJob(JobRelattionRequest request);
        public Task<JobRecommendedReponse> GetRecomendJob(JobRecommendedRequest request);



    }
}
