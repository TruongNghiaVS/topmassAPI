using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository.IndexModel;

namespace Topmass.Core.Repository
{
    public partial interface IJobLogViewRepository : IBaseRepository<JobLogViewModel>
    {
        public Task<SearchRepJobLogViewReponse> GetAll(SearchRepJobLogView request);


    }
}
