using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.CV;

namespace Topmass.Core.Repository
{
    public partial class OpenCVResultRepository : RepositoryBase<OpenCVResult>, IOpenCVResultRepository
    {
        public OpenCVResultRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "OpenCVResult";
        }

    }
}
//IOpenViewerResultRepository