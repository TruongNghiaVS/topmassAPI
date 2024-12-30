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
        private readonly IOpenCVResultRepository _openCVResultRepository;
        private readonly ISearchCVRepository _searchCVRepository;

        private readonly IOpenViewerResultRepository _openViewerResultRepository;
        public RewardBusiness(IRecruiterRepository userRepository,
             IRecruiterInfoRepository infoRepository,
             IRewardTransactionRepository rewardTransactionRepository,
             ISearchCVResultRepository searchCVResultRepository,
             IOpenCVResultRepository openCVResultRepository,
             ISearchCVRepository searchCVRepository,
             IOpenViewerResultRepository openViewerResultRepository

            )
        {
            _repository = userRepository;
            _infoRepository = infoRepository;
            _rewardTransactionRepository = rewardTransactionRepository;
            _searchCVResultRepository = searchCVResultRepository;
            _openCVResultRepository = openCVResultRepository;
            _searchCVRepository = searchCVRepository;
            _openViewerResultRepository = openViewerResultRepository;
        }

        public async Task<BaseResult> ExchangePointToOpenCVNoSearchCV(int
        searchId, int point, int userId, int identify, string fileName)
        {

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

            await _openCVResultRepository.ExecuteSqlProcedure("sp_ExchangePointToOpenCVNoSearchCV", new
            {
                searchId = searchId,
                point = point,
                userId = userId,
                identify = identify,
                linkfile = fileName
            });
            await _repository.AddOrUPdate(recruiterItem);
            return new BaseResult();
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

            var resultCheck = await _searchCVResultRepository.FindOneByStatementSql<OpenCVResult>(
               "select * from OpenCVResult where relId=  @searchId  and  CreatedBy = @userid",
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
                resultCheck = new OpenCVResult()
                {

                    CreateAt = DateTime.Now,
                    CreatedBy = userId,
                    RelId = searchId,
                    SearchId = searchId,
                    Status = 0,
                    UpdateAt = DateTime.Now,
                    UpdatedBy = userId,
                    Deleted = false
                };

            var searchResult = await _searchCVRepository.GetById(searchId);
            searchResult.CountContact++;
            await _searchCVRepository.AddOrUPdate(searchResult);
            await _openCVResultRepository.AddOrUPdate(resultCheck);
            return reponse;
        }

        public async Task<BaseResult> ExchangePointToOpenViewer(
            int userId, int point,
            int logviewerId)
        {
            var reponse = new BaseResult();
            if (logviewerId < 1)
            {
                reponse.Message = "Thiếu đối tượng mở";
                return reponse;
            }
            var recruiterItem = await _repository.GetById(userId);
            if (recruiterItem == null)
            {
                return reponse;
            }
            var resultCheck = await _rewardTransactionRepository.FindOneByStatementSql<OpenViewerResult>(
                    "select * from OpenViewerResult where viewId = @id", new
                    {
                        id = logviewerId
                    }
            );
            if (resultCheck != null && resultCheck.Id > 0)
            {
                if (resultCheck.Status != 2)
                {
                    return reponse;
                }
            }
            if (recruiterItem.NumberLightning < 1)
            {
                reponse.Message = "Không đủ tia sét để mở thông tin, vui lòng thu thập tia sét vả thử lại sau";
                return reponse;
            }
            recruiterItem.NumberLightning += -point;
            await _repository.AddOrUPdate(recruiterItem);
            var historyItem = new RewardTransaction()
            {
                BusinessDate = DateTime.Now,
                Content = "Dùng " + point + " tia sét để mở Ứng viên",
                CreateAt = DateTime.Now,
                DataType = 2,
                Rel = userId,
                Point = point,
                CreatedBy = userId,
                Status = 0,
                Deleted = false,
                UpdateAt = DateTime.Now,
                UpdatedBy = userId
            };
            var transaction = await _rewardTransactionRepository.AddAndGetId(historyItem);
            var openViewerResult = new OpenViewerResult()
            {
                TranSactionId = transaction,
                LockInfo = true,
                Status = 2,
                ViewId = logviewerId,
                RelId = userId,
                BussinessTime = DateTime.Now,
                UpdateAt = DateTime.Now,
                CreatedBy = userId,
                UpdatedBy = userId,
                CreateAt = DateTime.Now,
                Deleted = false,
            };
            await _openViewerResultRepository.AddOrUPdate(openViewerResult);
            return reponse;
        }


    }
}
