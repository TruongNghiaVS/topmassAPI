using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.CV;

namespace Topmass.Core.Repository
{
    public partial class OpenViewerResultRepository : RepositoryBase<OpenViewerResult>, IOpenViewerResultRepository
    {
        public OpenViewerResultRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "OpenViewerResult";
        }

    }
}
