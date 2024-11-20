

using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Reward;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public class ExchangeCVDetailRepository : RepositoryBase<ExchangeCVDetail>, IExchangeCVDetailRepository
    {
        public ExchangeCVDetailRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "exchangeCVDetail";
        }
    }
}
