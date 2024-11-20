using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.CV;

namespace Topmass.Core.Repository
{
    public partial class ResumeRepository : RepositoryBase<Resume>, IResumeRepository
    {
        public ResumeRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "resumes";
        }
    }
}
