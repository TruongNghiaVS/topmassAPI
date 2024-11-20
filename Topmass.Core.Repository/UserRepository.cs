using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public class UserRepository : RepositoryBase<UserModel>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "UserModel";
        }

        public async Task<bool> AddUser(UserAdd request)
        {
            return await this.ExceueProdure("pro_addUser", request);
        }


    }
}
