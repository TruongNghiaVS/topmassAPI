﻿using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository.IndexModel;
using Topmass.Core.Repository.Model;
using Topmass.Utility;

namespace Topmass.Core.Repository
{
    public partial class JobItemRepository : RepositoryBase<JobItemModel>, IJobRepository
    {


        public JobItemRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "jobItems";
        }

        public async Task<SearchRepJobReponse> SearchAll(SearchRepJobRequest request)
        {
            var reponse = new SearchRepJobReponse();
            var dataResult = await this.ExecuteSqlProcerduceToList<JobItemIndex>
                ("sp_job_searchAll", request, commandType: System.Data.CommandType.StoredProcedure);
            reponse.Data = dataResult;
            reponse.Limit = request.Limit;
            reponse.Page = request.Page;
            return reponse;
        }
        public async Task<JobItemModel> GetBySlug(string slug)
        {
            var reponse = new SearchRepJobReponse();
            var dataResult = await this.FindOneByStatementSql<JobItemModel>("Select * from jobItems d where d.Slug = @slug", new
            {
                slug
            });
            if (dataResult == null || dataResult.Id < 1)
            {
                return null;
            }
            return dataResult;
        }
        public async Task<string> CreateSlugJob(int humanId, string titile)
        {
            var slugtify = Utilities.SlugifySlug(titile);
            var dataResult = await this.FindOneByStatementSql<JobExtraIndex>
            (
            "select d.id, d.Name, e.FullName, " +
            " e.slug from Recruiter  d left join CompanyInfo e on d.id = e.RelId where d.id = @relId", new
            {
                relId = humanId
            });
            if (dataResult == null || dataResult.Id < 1)
            {
                return "";
            }


            var titleJob = titile + " " + dataResult.FullName;
            titleJob = Utilities.SlugifySlug(titleJob);
            return titleJob;

        }
    }
}
