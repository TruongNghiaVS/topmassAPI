

using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Reward;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public class ExchangeCVRepository : RepositoryBase<ExchangeCV>, IExchangeCVRepository
    {
        public ExchangeCVRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "exchangeCV";
        }
    }
}
