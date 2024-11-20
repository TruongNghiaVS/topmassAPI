using Topmass.Core.Repository.IndexModel;
using Topmass.Core.Repository.Report;

namespace Topmass.Core.Repository
{
    public partial interface IJobOverViewCounterRepository : IBaseRepository<JobOverViewCounterModel>
    {

        public Task<JobOverViewCounterReponse> GetAll(JobOverViewCounterRequest request);
    }
}
