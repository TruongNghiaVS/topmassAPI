using Topmass.Bussiness.Company.Model;
using Topmass.Core.Model;

namespace Topmass.Bussiness.Company
{
    public interface ICompanyBusiness
    {
        public Task<GetAllCompanyReponse> GetAllCompany(GetAllCompanyRequest request);
        public Task<CompanyInfoModel> GetCompanyBySlug(string slug);
        public Task<dynamic> GetInfomationDetail(string slug, int userId = -1);
        public Task<dynamic> GetAllJobOfCompany(string slug, int userId = -1, int location = -1, string keyword = "");

        public Task<dynamic> AddFollow(string slug, int userId);

        public Task<dynamic> AddFollow(int slug, int userId);

        public Task<dynamic> RemoveFolow(int slug, int userId);
    }
}
