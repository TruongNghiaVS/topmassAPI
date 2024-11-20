
using Topmass.Job.Business.Model;

namespace Topmass.Job.Business
{
    public interface IJobSearchBusiness
    {

        public Task<GetAllBestJobOptimizationReponse> GetAllBestJobOptimization(GetAllBestJobOptimizationRequest request);

        public Task<GetAllBestJobOptimizationReponse> GetSuitableJob(GetSuitableJobRequest request);

        public Task<GetAllBestJobOptimizationReponse> GetAttractiveJobs(GetAttractiveJobs request);


        public Task<GetAllBestJobOptimizationReponse> SearchJob(SearchJobRequest request);

    }
}
