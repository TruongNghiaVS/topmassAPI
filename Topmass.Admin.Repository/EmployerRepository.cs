using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Admin;
using Topmass.Core.Repository;

namespace Topmass.Campagn.Repository
{
    public class EmployerRepository : RepositoryBase<Employer>, IEmployeeeRepository
    {
        public EmployerRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "Employer";
        }


        public async Task<Employer> Login(string userName, string password)
        {
            var modelCheck = new
            {
                userName,
                password
            };
            var result = await ExecuteSQL<Employer>("select * from Employer where  UserName = @userName and Password = @password",
              modelCheck);
            return result;
        }
    }
}
