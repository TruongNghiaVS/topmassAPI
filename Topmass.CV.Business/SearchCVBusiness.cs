using Topmass.Core.Model.CV;
using Topmass.Core.Repository;
using Topmass.CV.Business.Model;
using Topmass.CV.Repository;

namespace Topmass.CV.Business
{
    public class SearchCVBusiness : ISearchCVBusiness
    {

        private readonly ISearchCVResultRepository searchCVResultRepository;
        private readonly ISearchCVRepository _cvRepository;

        public SearchCVBusiness(ICVRepository cVRepository,
            ISearchCVResultRepository _searchCVResultRepository,
            ISearchCVRepository cvRepository
            )
        {
            _cvRepository = cvRepository;
            searchCVResultRepository = _searchCVResultRepository;
        }

        public async Task<bool> SaveResultSearch(int searchId, string LinkFile, int userId,
            int campaignId = -1, int jobId = -1, bool lockInfo = true)
        {



            var resultCheck = await searchCVResultRepository.FindOneByStatementSql<SearchCVResultModel>(
                "select top 1 * from SearchResult where relId=  @searchId  and  CreatedBy = @userid",
                 new
                 {
                     searchId,
                     userid = userId
                 }
                );
            if (resultCheck == null)
            {
                resultCheck = new SearchCVResultModel()
                {
                    CreateAt = DateTime.Now,
                    CreatedBy = userId,
                    UpdateAt = DateTime.Now,
                    Status = 115
                };
            }
            if (resultCheck.CampaignId != campaignId)
            {
                resultCheck.Status = 115;
            }
            resultCheck.RelId = searchId;
            resultCheck.LinkFile = LinkFile;
            resultCheck.CampaignId = campaignId;
            resultCheck.Jobid = jobId;
            resultCheck.CreatedBy = userId;
            resultCheck.LockInfo = lockInfo;
            await searchCVResultRepository.AddOrUPdate(resultCheck);
            var infoCandidate = await _cvRepository.GetById(searchId);
            if (infoCandidate == null)
            {
                return true;
            }

            var searchInfo = await searchCVResultRepository.FindOneByStatementSql<SearchCVModel>(
             "select *  from SearchCV where id = @searchId",
              new
              {

                  searchId = searchId
              }
             );
            var sqlText = "sp_applyJobWithSearchCVv3";

            if (searchInfo.SourceType == 2)
            {
                sqlText = "sp_applyJobWithSearchCVvTypeCVUpload";
            }
            var createCV = await searchCVResultRepository.ExecuteStatementSql(sqlText, new
            {
                TemplateID = 1,
                searchId,
                LinkFile,
                UserId = userId,
                jobId,
                LockInfo = lockInfo

            });
            return true;
        }

        public async Task<CheckFileDigitalReponse> CheckFileDigitalCV(int searchId,
            bool lockfile = false)
        {
            var result = await _cvRepository.CheckReloadFile(searchId);
            var reponse = new CheckFileDigitalReponse();
            if (result == null || result.Id < 1)
            {
                reponse.IsCreateNewFile = true;
                reponse.LinkFile = "";
                return reponse;
            }
            if (lockfile == false && string.IsNullOrEmpty(result.FileCV))
            {
                reponse.IsCreateNewFile = true;
                reponse.LinkFile = "";
                return reponse;

            }

            if (lockfile == true && string.IsNullOrEmpty(result.FileCVHide))
            {
                reponse.IsCreateNewFile = true;
                reponse.LinkFile = "";
                return reponse;
            }
            if (lockfile == false)
            {
                reponse.IsCreateNewFile = false;
                reponse.LinkFile = result.FileCV;
                return reponse;
            }

            if (lockfile == true)
            {
                reponse.IsCreateNewFile = true;
                reponse.LinkFile = result.FileCVHide;
                return reponse;
            }
            return reponse;
        }
    }
}
