using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public interface IRecruiterRepository : IBaseRepository<RecruiterModel>
    {
        public Task<bool> AddUser(RecruiterRepAdd request);
        public Task<RecruiterModel2> FindUser(AuthenRepSearch request);


    }
}
