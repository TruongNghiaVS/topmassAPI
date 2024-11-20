
using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Sync.Repository
{
    public class SyncDaRepository : RepositoryBase<CandidateModel>, ISyncDaRepository
    {
        public SyncDaRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<List<AllIdCandidateNeedSync>> GetAllIdCandidateNeedSyncs()
        {
            var result = await ExecuteSqlProcerduceToList<AllIdCandidateNeedSync>("sp_candidate_getAllSync",
                new { }, System.Data.CommandType.StoredProcedure);

            if (result == null)
            {
                return new List<AllIdCandidateNeedSync>();
            }
            return result;
        }

        public async Task<bool> AddCVToSeachCV(object requestAdd)
        {

            await ExecuteSqlProcedure("AddCVToSearchCV", requestAdd);

            return true;
        }
    }
}
