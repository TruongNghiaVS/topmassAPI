using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository.IndexModel;

namespace Topmass.Core.Repository
{
    public partial interface IJobRepository : IBaseRepository<JobItemModel>
    {
        public Task<SearchRepJobReponse> SearchAll(SearchRepJobRequest request);
        public Task<JobItemModel> GetBySlug(string slug);



    }
}
