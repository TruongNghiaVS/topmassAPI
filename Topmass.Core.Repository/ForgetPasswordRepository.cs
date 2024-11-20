using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;

namespace Topmass.Core.Repository
{
    public class ForgetPasswordRepository : RepositoryBase<ForgetPasswordModel>, IForgetPasswordRepository
    {
        public ForgetPasswordRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "ForgetPassword";
        }




    }
}
