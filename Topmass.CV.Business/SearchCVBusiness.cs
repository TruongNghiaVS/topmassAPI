using Topmass.Core.Model.CV;
using Topmass.Core.Repository;
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
            int campaignId = -1, int jobId = -1)
        {
            var resultCheck = await searchCVResultRepository.FindOneByStatementSql<SearchCVResultModel>(
                "select * from SearchResult where relId=  @searchId  and  CreatedBy = @userid",
                 new
                 {
                     searchId,
                     userid = userId
                 }
                );

            if (resultCheck == null || resultCheck.Id > 0)
            {

            }
            else
            {
                resultCheck.Status = 85;
                resultCheck.RelId = searchId;
                resultCheck.LinkFile = LinkFile;
                resultCheck.CampaignId = campaignId;
                resultCheck.Jobid = jobId;
                resultCheck.CreatedBy = userId;
                await searchCVResultRepository.AddOrUPdate(resultCheck);
                return true;
            }
            var result = new SearchCVResultModel()
            {
                CampaignId = campaignId,
                CreateAt = DateTime.Now,
                CreatedBy = userId,
                RelId = searchId,
                Deleted = false,
                SearchId = searchId,
                Status = 85,
                LinkFile = LinkFile,
                UpdateAt = DateTime.Now,
                UpdatedBy = userId,
                Jobid = jobId
            };
            await searchCVResultRepository.AddOrUPdate(result);
            var infoCandidate = await _cvRepository.GetById(searchId);
            if (infoCandidate == null)
            {
                return true;
            }

            var createCV = await searchCVResultRepository.ExecuteStatementSql("sp_applyJobWithSearchCVv2", new
            {
                TypeData = 3,
                TemplateID = 1,
                LinkFile = LinkFile,
                FullName = infoCandidate.FullName,
                Phone = infoCandidate.Phone,
                Email = infoCandidate.Email,
                UserId = infoCandidate.CandidateId,
                jobId = jobId
            });

            return true;
        }


    }
}
