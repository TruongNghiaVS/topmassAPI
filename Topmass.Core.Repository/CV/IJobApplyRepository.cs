using Topmass.Core.Model.JobAply;

namespace Topmass.Core.Repository
{
    public partial interface IJobApplyRepository : IBaseRepository<JobApply>
    {
        public Task<bool> AddCounterApply(int jobIdd, int UserId);
    }
}
