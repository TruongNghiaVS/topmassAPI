using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public class CandidateRepository : RepositoryBase<CandidateModel>, ICandidateRepository
    {
        public CandidateRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "Candidate";
        }

        public async Task<bool> AddUser(CandidateAdd request)
        {
            return await this.ExceueProdure("sp_Candidate_register", request);
        }
        public async Task<CandidateModel> FindUser(CandidateSearch request)
        {
            var result = await this.ExecuteSQL<CandidateModel>("sp_Candidate_search", request);
            return result;

        }




    }
}
