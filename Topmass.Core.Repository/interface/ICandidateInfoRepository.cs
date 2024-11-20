using Topmass.Core.Model;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial interface ICandidateInfoRepository : IBaseRepository<CandidateInfoModel>
    {
        public Task<bool> UpdateInfoCandidate(CandidateInfoUpdateRequest requestUpdate);
        public Task<CandidateInfoModel> GetInfo(string email = null);

    }
}
