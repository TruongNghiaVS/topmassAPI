using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository.IndexModel;

namespace Topmass.Core.Repository
{
    public partial class JobDisplayItemRepository : RepositoryBase<JobDisplayItemModel>, IJobDisplayItemRepository
    {
        public JobDisplayItemRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "jobItemDisplay";
        }

        public async Task<SearchRepJobLogViewReponse> GetAll(SearchRepJobLogView request)
        {
            var reponse = new SearchRepJobLogViewReponse();
            var dataResult = await ExecuteSqlProcerduceToList<JobLogViewIndexModel>
                ("sp_jobLogview_searchAll",
                request,
                commandType: System.Data.CommandType.StoredProcedure);
            reponse.Data = dataResult;
            reponse.Limit = request.Limit;
            reponse.Page = request.Page;
            return reponse;
        }





    }
}
