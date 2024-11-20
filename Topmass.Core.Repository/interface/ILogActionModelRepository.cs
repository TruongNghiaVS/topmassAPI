using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial interface ILogActionModelRepository : IBaseRepository<LogActionModel>
    {
        public Task<List<LogActionModel>> GetAllHistory(GetAllHistoryRequest request);

    }
}
