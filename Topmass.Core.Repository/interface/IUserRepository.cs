using Topmass.Core.Model;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial interface IUserRepository : IBaseRepository<UserModel>
    {

        public Task<bool> AddUser(UserAdd request);



    }
}
