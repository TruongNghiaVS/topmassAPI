using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.CV;

namespace Topmass.Core.Repository
{
    public partial class ResumeUIRepository : RepositoryBase<ResumeUI>, IResumeUIRepository
    {
        public ResumeUIRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "resumeUI";
        }
    }
}
