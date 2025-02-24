

using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public class RecruiterRepository : RepositoryBase<RecruiterModel>, IRecruiterRepository
    {
        public RecruiterRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "Recruiter";
        }

        public async Task<bool> AddUser(RecruiterRepAdd request)
        {
            return await this.ExceueProdure("sp_Recruiter_register", request);
        }
        public async Task<RecruiterModel2> FindUser(AuthenRepSearch request)
        {
            var result = await this.ExecuteSQL<RecruiterModel2>("sp_Recruiter_search", request);
            return result;

        }
    }
}
