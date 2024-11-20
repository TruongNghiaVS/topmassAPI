using Topmass.Core.Model;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial interface ICandidateRepository : IBaseRepository<CandidateModel>
    {
        public Task<bool> AddUser(CandidateAdd request);
        public Task<CandidateModel> FindUser(CandidateSearch request);
    }
}
