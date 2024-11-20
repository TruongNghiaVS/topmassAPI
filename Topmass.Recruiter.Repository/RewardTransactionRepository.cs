

using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Reward;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public class RewardTransactionRepository : RepositoryBase<RewardTransaction>, IRewardTransactionRepository
    {
        public RewardTransactionRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "RewardTransaction";
        }

    }
}
