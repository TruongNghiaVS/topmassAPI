using Topmass.Core.Model.CV;
using Topmass.Core.Model.Reward;
using Topmass.Core.Repository;
using Topmass.Recruiter.Repository;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{
    public partial class RewardBusiness : IRewardBusiness
    {
        private readonly IRecruiterRepository _repository;
        private readonly IRecruiterInfoRepository _infoRepository;
        private readonly IRewardTransactionRepository _rewardTransactionRepository;
        private readonly ISearchCVResultRepository _searchCVResultRepository;
        public RewardBusiness(IRecruiterRepository userRepository,
             IRecruiterInfoRepository infoRepository,
             IRewardTransactionRepository rewardTransactionRepository,
             ISearchCVResultRepository searchCVResultRepository

            )
        {
            _repository = userRepository;
            _infoRepository = infoRepository;
            _rewardTransactionRepository = rewardTransactionRepository;
            _searchCVResultRepository = searchCVResultRepository;
        }
        public async Task<BaseResult> ExchangePointToOpenCV(int searchId,
            int point,
            int userId,
            int? campaignId = -1)
        {

            if (!campaignId.HasValue)
            {
                campaignId = -1;
            }
            var reponse = new BaseResult();
            var recruiterItem = await _repository.GetById(userId);
            if (recruiterItem == null)
            {
                return reponse;
            }
            if (recruiterItem.NumberLightning < 1)
            {
                reponse.AddError("reward", "Không đủ tia sét để mở CV, vui lòng thu thập tia sét thử sa");
            }
            if (recruiterItem.NumberLightning <= point)
            {
                reponse.AddError("reward", "Không đủ tia sét để mở CV, vui lòng thu thập thêm tia sét để mở");
            }
            recruiterItem.NumberLightning += -point;
            await _repository.AddOrUPdate(recruiterItem);
            var historyItem = new RewardTransaction()
            {
                BusinessDate = DateTime.Now,
                Content = "Dùng " + point + " tia sét để mở CV",
                CreateAt = DateTime.Now,
                Rel = userId,
                Point = point,
                CreatedBy = userId,
                Status = 0,
                Deleted = false,
                UpdateAt = DateTime.Now,
                UpdatedBy = userId
            };
            await _rewardTransactionRepository.AddOrUPdate(historyItem);

            var resultCheck = await _searchCVResultRepository.FindOneByStatementSql<SearchCVResultModel>(
               "select * from SearchResult where relId=  @searchId  and  CreatedBy = @userid",
                new
                {
                    searchId = searchId,
                    CreatedBy = userId
                }
               );

            if (resultCheck.Id > 0)
            {
                resultCheck.Status = 0;
            }

            else
                resultCheck = new SearchCVResultModel()
                {
                    CampaignId = campaignId.Value,
                    CreateAt = DateTime.Now,
                    CreatedBy = userId,
                    RelId = searchId,
                    SearchId = searchId,
                    Status = 0,
                    UpdateAt = DateTime.Now,
                    UpdatedBy = userId,
                    Deleted = false
                };
            await _searchCVResultRepository.AddOrUPdate(resultCheck);
            return reponse;
        }




    }
}
