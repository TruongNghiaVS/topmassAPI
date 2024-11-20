using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Sync.Repository
{
    public interface ISyncDaRepository : IBaseRepository<CandidateModel>
    {
        public Task<List<AllIdCandidateNeedSync>> GetAllIdCandidateNeedSyncs();

        public Task<bool> AddCVToSeachCV(object requestAdd);

    }
}
