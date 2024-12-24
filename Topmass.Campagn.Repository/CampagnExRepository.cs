using Microsoft.Extensions.Configuration;
using Topmass.Campagn.Repository.Model;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;

namespace Topmass.Campagn.Repository
{
    public partial class CampagnExRepository : RepositoryBase<CampagnModel>, ICampagnExRepository
    {
        private readonly IJobRepository _jobRepository;
        public CampagnExRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
        {
            tableName = "Campaign";
            _jobRepository = jobRepository;
        }

        public async Task<CampangnSearchJobReponse> GetAllJob(SearchJobByCampagn request)
        {
            var data = await ExecuteSqlProcerduceToList<JobItemDisplay>("sp_searchJobofCampagn",
            request, System.Data.CommandType.StoredProcedure
           );

            var reponse = new CampangnSearchJobReponse();
            if (data.Count > 0)
            {
                reponse.Total = data.FirstOrDefault().TotalRecord;
            }
            reponse.Data = data;
            return reponse;
        }


        public async Task<CampangnSearchReponse> GetAll(SearchCampagnRequest request)
        {
            var data = await ExecuteSqlProcerduceToList<CampagnItemDisplay>("sp_campaign_getAll",
            request, System.Data.CommandType.StoredProcedure
            );

            foreach (var item in data)
            {
                var childInfo = await FindOneByStatementSql<NewsItem>("select top 1 id, Name   from jobItems where Campagn = @campaignId  order by id desc",
                    new { campaignId = item.Id });
                if (childInfo == null)
                {
                    childInfo = new NewsItem()
                    {
                        Id = -1,
                        Name = ""
                    };
                }
                item.ChildItems = childInfo;
            }
            var reponse = new CampangnSearchReponse();
            if (data.Count > 0)
            {
                reponse.Total = data.FirstOrDefault().TotalRecord;
            }
            reponse.Data = data;
            return reponse;
        }
    }
}
