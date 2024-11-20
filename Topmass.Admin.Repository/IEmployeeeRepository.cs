using Topmass.Core.Model.Admin;
using Topmass.Core.Repository;

namespace Topmass.Campagn.Repository
{
    public interface IEmployeeeRepository : IBaseRepository<Employer>
    {
        Task<Employer> Login(string userName, string password);

    }
}
