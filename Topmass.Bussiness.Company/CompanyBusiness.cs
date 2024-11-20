using System.Net;
using Topmass.Bussiness.Company.Model;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Bussiness.Company
{
    public class CompanyBusiness : ICompanyBusiness
    {

        private readonly ICompanyInfoRepository _repository;
        private readonly ICompanyFollowModelRepository _companyFollowModelRepository;
        private readonly ICompanyFavoriteModelRepository _companyFavoriteModelRepository;

        public CompanyBusiness(
              ICompanyInfoRepository companyInfoRepository,
              ICompanyFollowModelRepository companyFollowModelRepository,
              ICompanyFavoriteModelRepository companyFavoriteModelRepository

              )
        {
            _repository = companyInfoRepository;
            _companyFollowModelRepository = companyFollowModelRepository;
            _companyFavoriteModelRepository = companyFavoriteModelRepository;
        }

        public async Task<bool> LinkExists(string imageUrlAddress)
        {
            if (string.IsNullOrEmpty(imageUrlAddress))
            {
                return false;
            }
            WebRequest webRequest = WebRequest.Create(imageUrlAddress);
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch //If exception thrown then couldn't get response from address 
            {
                return false;
            }
            return true;
        }

        public async Task<GetAllCompanyReponse> GetAllCompany(GetAllCompanyRequest request)
        {

            var allData = await _repository.ExecuteSqlProcerduceToList<CompanyItemDisplay>("sql_getAllCompany",
                new
                {
                    request.Keyword,
                    request.UserId
                }
            );

            foreach (var item in allData)
            {
                var validImage = await LinkExists(item.CoverFullLink);
                if (!validImage)
                {
                    item.CoverLink = "/default/defaultCompany.jpg";
                }

                if (request.LoadJob == false)
                    continue;

                item.DataJob = await _repository.ExecuteSqlProcerduceToList<BaseJobItem>("sq_getListJobOfcompany",
                new
                {
                    companyId = item.id
                });

            }
            var reponse = new GetAllCompanyReponse()
            {
                Data = allData

            };
            return reponse;

        }
        public async Task<CompanyInfoModel> GetCompanyBySlug(string slug)
        {

            var dataCompany = await _repository.FindOneByStatementSql<CompanyInfoModel>("select top 1 * from CompanyInfo  where slug = @slug",
                new
                {
                    slug = slug
                });
            if (dataCompany == null || dataCompany.Id < 1)
            {
                return null;
            }
            return dataCompany;
        }

        public async Task<dynamic> GetInfomationDetail(string slug, int userId = -1)
        {
            var dataCompany = await GetCompanyBySlug(slug);
            if (dataCompany == null)
            {
                return new GetInfomationDetailReponse()
                {
                    Data = new CompanyDetailDisplay()
                };
            }
            var reponse = new GetInfomationDetailReponse();
            var data = new CompanyDetailDisplay()
            {
                AddressInfo = dataCompany.AddressInfo,
                Capacity = dataCompany.Capacity,
                CoverImage = dataCompany.CoverLink,
                Logo = dataCompany.LogoLink,
                CountFollow = 1000,
                Email = dataCompany.EmailCompany,
                Introduction = dataCompany.shortDes,
                MapInfo = dataCompany.MapInfo,
                Website = dataCompany.Website,
                Name = dataCompany.FullName
            };
            data.IsFollow = false;
            if (userId > 0)
            {
                var follow = await _companyFollowModelRepository.FindOneByStatementSql<CompanyFollowModel>
                   ("select top 1 * from CompanyFollow where RelId =@RelId and userId = @userId",
                   new { RelId = dataCompany.RelId, userId = userId }

                   );

                if (follow != null)
                {
                    data.IsFollow = true;
                }
            }
            reponse.Data = data;
            return reponse;
        }

        public async Task<dynamic> GetAllJob(string slug)
        {

            var dataCompany = await GetCompanyBySlug(slug);
            if (dataCompany == null)
            {
                return new GetInfomationDetailReponse()
                {
                    Data = new CompanyDetailDisplay()
                };
            }

            var reponse = new GetInfomationDetailReponse();

            var data = new CompanyDetailDisplay()
            {
                AddressInfo = dataCompany.AddressInfo,
                Capacity = dataCompany.Capacity,
                CoverImage = dataCompany.CoverLink,
                Logo = dataCompany.LogoLink,
                CountFollow = 1000,
                Email = dataCompany.EmailCompany,
                Introduction = dataCompany.shortDes,
                MapInfo = "https://www.google.com.vn/",
                Website = dataCompany.Website,
            };
            reponse.Data = data;
            return reponse;
        }

        private string GetFullLink(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink))
            {
                return "";
            }
            return "https://www.cdn.topmass.vn/static/" + shortLink;
        }




        public async Task<dynamic> GetAllJobOfCompany(string slug, int userId = -1, int location = -1, string keyword = "")
        {

            var dataCompany = await GetCompanyBySlug(slug);

            if (dataCompany == null)
            {
                return new List<object>();
            }
            var allJobIdSave = new List<JobIdCount>();
            var allJobApply = new List<JobIdCount>();
            var allJob = await _repository.ExecuteSqlProcerduceToList<JobCompanyItemDisplay>("sp_GetAllJobOfCompany",
            new
            {
                companyId = dataCompany.RelId,
                location = location,
                keyword = keyword
            });
            if (userId > 0)
            {
                allJobIdSave = await _repository.ExecuteSqlProcerduceToList<JobIdCount>
                 (
              "select DISTINCT JobId from jobSave where JobId in ( select id from jobItems where  RelId = @RelId ) and UserId = @UserId and Deleted  =0 ",
              new { RelId = dataCompany.RelId, UserId = userId },
              commandType: System.Data.CommandType.Text

              );

                allJobApply = await _repository.ExecuteSqlProcerduceToList<JobIdCount>
                    (
                    "select DISTINCT  JobId  from jobApply  where JobId in  ( select  id from jobItems where RelId = @RelId) and CreatedBy = @userId ", new { RelId = dataCompany.RelId, UserId = userId },
                    commandType: System.Data.CommandType.Text
                    );

                var listNew = new List<object>();
                foreach (var item in allJob)
                {
                    var itemSave = allJobIdSave.Any(x => x.JobId == item.JobId);
                    var itemApply = allJobApply.Any(x => x.JobId == item.JobId);
                    item.IsJobSave = itemSave;
                    item.IsJobApply = itemApply;
                    item.IsLike = itemSave;
                    item.IsSave = itemSave;
                    item.IsApply = itemApply;

                    listNew.Add(item);
                }

                return listNew;
            }
            return allJob;
        }

        public async Task<dynamic> AddFavorite(string slug, int userId)
        {
            var company = await GetCompanyBySlug(slug);
            if (company == null)
            {
                return false;
            }

            var itemFavorite = await _companyFavoriteModelRepository.FindOneByStatementSql<CompanyFavoriteModel>(
                "select top 1 * from CompanyFavorite where RelId =@RelId and userId = @userId",
                new { RelId = company.RelId, userId = userId }
                );

            if (itemFavorite == null)
            {
                itemFavorite = new CompanyFavoriteModel();
                itemFavorite.RelId = company.RelId;
                itemFavorite.UserId = userId;
                itemFavorite.CreatedBy = userId;
                itemFavorite.CreateAt = DateTime.Now;
            }
            await _companyFavoriteModelRepository.AddOrUPdate(itemFavorite);
            return true;
        }
        public async Task<dynamic> AddFollow(string slug, int userId)
        {
            var company = await GetCompanyBySlug(slug);
            if (company == null)
            {
                return false;
            }

            var itemFavorite = await _companyFollowModelRepository.FindOneByStatementSql<CompanyFollowModel>(
                "select top 1 * from CompanyFollow where RelId =@RelId and userId = @userId",
                new { RelId = company.RelId, userId = userId }
                );

            if (itemFavorite == null)
            {
                itemFavorite = new CompanyFollowModel();
                itemFavorite.RelId = company.RelId;
                itemFavorite.UserId = userId;
                itemFavorite.CreatedBy = userId;
                itemFavorite.CreateAt = DateTime.Now;
            }
            await _companyFollowModelRepository.AddOrUPdate(itemFavorite);
            return true;
        }

        public async Task<dynamic> AddFollow(int slug, int userId)
        {

            var itemFavorite = await _companyFollowModelRepository.FindOneByStatementSql<CompanyFollowModel>(
                "select top 1 * from CompanyFollow where RelId =@RelId and userId = @userId and Deleted = 0 ",
                new { RelId = slug, userId = userId }
                );

            if (itemFavorite == null)
            {
                itemFavorite = new CompanyFollowModel();
                itemFavorite.RelId = slug;
                itemFavorite.UserId = userId;
                itemFavorite.CreatedBy = userId;
                itemFavorite.CreateAt = DateTime.Now;
            }
            await _companyFollowModelRepository.AddOrUPdate(itemFavorite);
            return true;
        }

        public async Task<dynamic> RemoveFolow(int slug, int userId)
        {
            var itemFavorite = await _companyFollowModelRepository.FindOneByStatementSql<CompanyFollowModel>(
                "select top 1 * from CompanyFollow where RelId =@RelId and userId = @userId and Deleted = 0 ",
                new { RelId = slug, userId = userId }
                );

            if (itemFavorite == null)
            {
                return false;
            }
            itemFavorite.Deleted = true;
            await _companyFollowModelRepository.AddOrUPdate(itemFavorite);
            return true;
        }


    }
}
