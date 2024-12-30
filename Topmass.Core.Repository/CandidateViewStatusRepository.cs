using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.JobAply;

namespace Topmass.Core.Repository
{
    public class CandidateViewStatusRepository : RepositoryBase<CandidateViewStatus>, IcandidateViewStatusRepository
    {
        public CandidateViewStatusRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "CandidateViewStatus";
        }




    }
}
