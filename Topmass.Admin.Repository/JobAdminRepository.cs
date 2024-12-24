using Microsoft.Extensions.Configuration;
using Topmass.Admin.Repository;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;

namespace Topmass.Campagn.Repository
{
    public partial class JobAdminRepository : RepositoryBase<CampagnModel>, IJobAdminRepository
    {

        private readonly IJobRepository _jobRepository;
        public JobAdminRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
        {
            _jobRepository = jobRepository;
        }
        public async Task<SearchJobAdminReponse> GetAll(dynamic request)
        {

            var result = await ExecuteSqlProcerduceToList<JobAdminItemDisplay>("sp_jobadmin_getall",
                request, System.Data.CommandType.StoredProcedure);
            var reponse = new SearchJobAdminReponse();
            reponse.Data = result;
            return reponse;
        }

        public async Task<List<JobLogAdminItemDisplay>> GetAllLog(int id)
        {
            var result = await ExecuteSqlProcerduceToList<JobLogAdminItemDisplay>("sp_jobadmin_getallLog",
                new { id = id }, System.Data.CommandType.StoredProcedure);

            return result;
        }

        public async Task<BasicInfoJobAdmin> GetDetailBasic(int id)
        {
            var result = await ExecuteSqlProcerduceAndGetOan<BasicInfoJobAdmin>("sp_admin_job_getbasicInfo", new { id });
            return result;
        }

        public async Task<DataInfoJobAdmin> GetInfoJobAdmin(int id)
        {

            var result = await ExecuteSqlProcerduceAndGetOan<DataInfoJobAdmin>("sp_admin_job_getInfo", new { id });
            return result;
        }
        public async Task<CompanyInfoNTD> GetCompanyInfo(int id)
        {
            var result = await ExecuteSqlProcerduceAndGetOan<CompanyInfoNTD>("sp_admin_ntd_getCompanyinfo", new { id });
            return result;
        }
        public async Task<DocumentNTDInfo> GetDocumentNTD(int id)
        {
            var result = await ExecuteSqlProcerduceAndGetOan<DocumentNTDInfo>("sp_admin_ntd_getDocumentinfo", new { id });
            return result;
        }
        public async Task<bool> UpdateDocumentNTD(string StatusChange,
          string NotedChange,
          int Id,
          string content)
        {

            var result = await ExecuteSqlProcedure("sp_adminUpdateBusinessLicenseLog",
            new
            {
                StatusChange,
                NotedChange,
                Id,
                content

            });

            return result;

        }
        public async Task<bool> AddDocumentNTD(string StatusChange, string NotedChange, int Id, string content, string linkFile)
        {
            var result = await ExecuteSqlProcedure("sp_adminAddBusinessLicenseLog",
            new
            {
                StatusChange,
                NotedChange,
                Id,
                content
            });
            return result;
        }
        public async Task<bool> UpdateInfoHuman(int statusAccout, int statusConfirm, int id)
        {
            var result = await ExecuteSqlProcedure("sp_adminUpdateInfoHuman",
            new
            {
                statusAccout,
                statusConfirm,
                id
            });
            return result;
        }
    }
}
