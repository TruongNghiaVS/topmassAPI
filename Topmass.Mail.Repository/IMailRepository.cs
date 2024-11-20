using Topmass.Core.Model;
using Topmass.Core.Repository;
using Topmass.Core.Repository.Model;

namespace Topmass.Mail.Repository
{
    public partial interface IMailRepository : IBaseRepository<MailInfoModel>
    {
        public Task<bool> AddMailInfo(MailInfoRepAdd request);
        public Task<List<MailInfoModel>> GetMailImp();
    }
}
