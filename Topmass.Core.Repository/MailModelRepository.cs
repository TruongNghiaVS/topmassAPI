using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.MailModel;

namespace Topmass.Core.Repository
{
    public partial class MailModelRepository : RepositoryBase<MailModel>, IMailModelRepository
    {


        public MailModelRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
        {
            tableName = "MailQueued";

        }


    }
}
