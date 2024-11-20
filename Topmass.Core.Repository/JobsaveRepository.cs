using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Campagn;

namespace Topmass.Core.Repository
{
    public partial class JobsaveRepository : RepositoryBase<JobsaveModel>, IJobsaveRepository
    {

        private readonly IJobRepository _jobRepository;
        public JobsaveRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
        {
            tableName = "jobSave";
            _jobRepository = jobRepository;
        }


    }
}
