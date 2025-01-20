using Microsoft.Extensions.Configuration;
using Topmass.Admin.Repository;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;

namespace Topmass.Campagn.Repository
{
    public partial class NTDRepository : RepositoryBase<CampagnModel>, INTDRepository
    {
        private readonly IJobRepository _jobRepository;
        public NTDRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
        {
            _jobRepository = jobRepository;
        }
        public async Task<SearchNTDReponse> GetAll(dynamic request)
        {
            //CbDocumnetStatus
            var result = await ExecuteSqlProcerduceToList<NTDItemDisplay>("sp_ntd_getall",
                request, System.Data.CommandType.StoredProcedure);
            var reponse = new SearchNTDReponse();
            reponse.Data = result;
            return reponse;
        }
        public async Task<List<NTDLogInfo>> GetAllLog(int id)
        {
            var result = await ExecuteSqlProcerduceToList<NTDLogInfo>("sp_ntd_getallLog",
               new { id = id }, System.Data.CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<NTDAccountLogInfo>> GetALlLogAccount(int id)
        {
            var result = await ExecuteSqlProcerduceToList<NTDAccountLogInfo>("sp_ntd_getallLogAccount",
               new { id = id }, System.Data.CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<NTDShortInfo>> GetAllShortNTD()
        {
            var result = await ExecuteSqlProcerduceToList<NTDShortInfo>("sp_ntd_getShortinfo",
                new { }, System.Data.CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<BasicInfoNTD> GetDetailBasic(int id)
        {
            var result = await ExecuteSqlProcerduceAndGetOan<BasicInfoNTD>("sp_admin_ntd_getbasicInfo", new { id });
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
          string content, int reasonReject)
        {

            var result = await ExecuteSqlProcedure("sp_adminUpdateBusinessLicenseLog",
            new
            {
                StatusChange,
                NotedChange,
                Id,
                content,
                reasonReject

            });
            return result;

        }

        public async Task<bool> AddDocumentNTD(string StatusChange,
        string NotedChange,
        int Id,
        string content,
        string linkFile, int reasonReject)
        {
            var result = await ExecuteSqlProcedure("sp_adminAddBusinessLicenseLog",
            new
            {
                StatusChange,
                NotedChange,
                Id,
                content,
                reasonReject
            });
            return result;
        }
        public async Task<bool> UpdateInfoHuman(int statusAccout, int statusConfirm, int id, int reasonCode = -1, string noted = "")
        {
            var result = await ExecuteSqlProcedure("sp_adminUpdateInfoHuman",
            new
            {
                statusAccout,
                statusConfirm,
                id,
                reasonCode,
                noted

            });
            return result;
        }

        public async Task<bool> UpdatePersonalPerson(string FullName, int Gender, string phoneNumber, int id)
        {
            var result = await ExecuteSqlProcedure("sp_adminUpdatePersonal",
            new
            {
                FullName = FullName,
                Gender = Gender,
                phoneNumber = phoneNumber,
                id = id


            });
            return result;
        }



        public async Task<bool> UpdateConfirmStatus(int id, int statusChange, string noted, string content)
        {

            var result = await ExecuteSqlProcedure("sp_adminUpdateJobConfirmStatus",
            new
            {
                statusChange,
                noted,
                id,
                content
            });
            return result;
        }
        public async Task<bool> UpdateStatusDisplay(int id, int statusChange, string noted = "", string content = "")
        {
            var result = await ExecuteSqlProcedure("sp_adminUpdateStatusDisplay",
            new
            {
                statusChange,
                noted,
                id,
                content
            });
            return result;
        }

        public async Task<bool> UpdateCompanyInfo(dynamic requestUpdate)
        {
            var result = await ExecuteSqlProcedure("sp_adminUpdateCopmpanyInfo", requestUpdate);
            return result;
        }

    }
}
