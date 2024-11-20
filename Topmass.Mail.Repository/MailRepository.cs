

using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository;
using Topmass.Core.Repository.Model;

namespace Topmass.Mail.Repository
{
    public class MailRepository : RepositoryBase<MailInfoModel>, IMailRepository
    {
        public MailRepository(IConfiguration configuration)
                : base(configuration)
        {
            tableName = "mailInfo";
        }

        public async Task<bool> AddMailInfo(MailInfoRepAdd request)
        {
            var mailInfoModel = new MailInfoModel()
            {
                Content = request.Content,
                CreatedBy = 1,
                MailTo = request.MailTo,
                CreateAt = DateTime.Now,
                TypeContent = request.TypeContent,
                Deleted = false,
                Status = 1,
                Subject = request.Subject,
                UpdateAt = DateTime.Now,
                UpdatedBy = 1
            };
            return await this.AddOrUPdate(mailInfoModel);
        }
        public async Task<List<MailInfoModel>> GetMailImp()
        {
            var result = await this.ExecutePro<MailInfoModel>("sp_GetMailImp", new { });
            if (result == null)
            {
                return new List<MailInfoModel>();
            }
            return result.ToList();
        }
    }
}
