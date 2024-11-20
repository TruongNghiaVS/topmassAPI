using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository.IndexModel;

namespace Topmass.Core.Repository
{
    public partial class JobLogViewRepository : RepositoryBase<JobLogViewModel>, IJobLogViewRepository
    {
        public JobLogViewRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "JobLogView";
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
