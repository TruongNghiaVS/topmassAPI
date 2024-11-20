using Topmass.Core.Model.CV;
using Topmass.Core.Model.Profile;
using Topmass.Core.Repository;
using Topmass.CV.Business.Model;
using TopMass.Core.Result;

namespace Topmass.CV.Business
{
    public class ProfileCVBusiness : IProfileCVBusiness
    {

        private readonly ICandidateRepository _repository;
        private readonly ISearchCVExtraRepository _searchCVExtraRepository;
        private readonly ISearchCVRepository _searchCVRepository;
        private readonly ISearchCVResultRepository _searchCVResultRepository;

        public ProfileCVBusiness(ICandidateRepository cVRepository,
            ISearchCVExtraRepository searchCVExtraRepository,
            ISearchCVRepository searchCVRepository,
            ISearchCVResultRepository searchCVResultRepository
            )
        {
            _repository = cVRepository;
            _searchCVExtraRepository = searchCVExtraRepository;
            _searchCVRepository = searchCVRepository;
            _searchCVResultRepository = searchCVResultRepository;
        }

        public async Task<EducationGetAllResult> GetAllEducation(int userId)
        {
            var data = await _repository.GetAllByStatementSql<EducationDisplayItem>(
                "select * from educationUser where relId = @userId order by id desc",
                new
                {
                    userId
                }
             );
            var reponse = new EducationGetAllResult()
            {
                Data = data
            };
            return reponse;

        }

        public async Task<ExperienceGetAllResult> GetAllExperience(int userId)
        {
            var data = await _repository.GetAllByStatementSql<ExperienceDisplayItem>(
                "select * from experienceUser where relId = @userId order by id desc",
                new
                {
                    userId
                }
             );
            var reponse = new ExperienceGetAllResult()
            {
                Data = data
            };
            return reponse;

        }

        public async Task<ProjectUserGetAllResult> GetAllProjectUser(int userId)
        {
            var data = await _repository.GetAllByStatementSql<ProjectUserDisplayItem>(
                "select * from ProjectUser where relId = @userId order by id desc",
                new
                {
                    userId
                }
             );
            var reponse = new ProjectUserGetAllResult()
            {
                Data = data
            };
            return reponse;

        }


        public async Task<OtherUserProfileGetAllResult> GetAllOtherProfileUser(int typeData, int userId)
        {
            if (typeData == 1)
            {
                var data1 = await _repository.GetAllByStatementSql<OtherUserProfileSkillDisplayItem>(
                "select * from OtherProfileUser where typedata =@typedata and  createdBy = @userId order by id desc",
                new
                {
                    typedata = typeData,
                    userId
                }
                );
                var reponse = new OtherUserProfileGetAllResult()
                {
                    Data = data1
                };
                return reponse;
            }
            var data = await _repository.GetAllByStatementSql<OtherUserProfileDisplayItem>(
            "select * from OtherProfileUser where typedata =@typedata and  createdBy = @userId order by id desc",
            new
            {
                typedata = typeData,
                userId
            }
            );
            return new OtherUserProfileGetAllResult()
            {
                Data = data
            };
        }


        public async Task<CertifyUserGetAllResult> GetAllCertifyUser(int userId, int typedata)
        {
            var data = await _repository.GetAllByStatementSql<CertifyUserDisplayItem>(
                "select * from CertifyUser where relId = @userId and typeData = @typeData order by id desc",
                new
                {
                    userId,
                    typeData = typedata
                }
             );
            var reponse = new CertifyUserGetAllResult()
            {
                Data = data
            };
            return reponse;

        }
        public async Task<dynamic> GetProfileUserCV(int userId)
        {
            var profileUser = await _repository.FindOneByStatementSql<ProfileCVUser>("select * from ProfileCV where RelId = @userId",
            new
            {
                userId
            }
            );
            if (profileUser != null)
            {
                return profileUser;
            }
            profileUser = new ProfileCVUser();
            return profileUser;
        }
        public async Task<dynamic> GetFullProfileUser(string searchId, int userHuamnId)
        {
            var data = await _repository.FindOneByStatementSql<SearchCVModel>
            ("select * from SearchCV where id = @searchId", new
            {
                searchId = searchId
            });
            var userId = -1;
            if (data != null)
            {
                userId = data.CandidateId;
            }
            var baseReult = new BaseResult();
            var educations = await GetAllEducation(userId);
            var experiences = await GetAllExperience(userId);
            var allProjects = await GetAllProjectUser(userId);
            var allSkill = await GetAllOtherProfileUser(1, userId);
            var allsoftSkill = await GetAllOtherProfileUser(2, userId);
            var allTools = await GetAllOtherProfileUser(3, userId);
            var allReward = await GetAllCertifyUser(userId, 2);
            var allCertify = await GetAllCertifyUser(userId, 1);
            var profileCv = await GetProfileUserCV(userId);


            var resultCheck = await _searchCVResultRepository.FindOneByStatementSql<SearchCVResultModel>(
              "select * from SearchResult where relId=  @searchId  and  CreatedBy = @userid",
              new
              {
                  searchId,
                  userid = userHuamnId
              }
          );

            var hideInfo = true;
            if (resultCheck != null && resultCheck.Id > 0)
            {
                hideInfo = false;
            }

            var dataInfo = new
            {
                educations = educations.Data,
                experiences = experiences.Data,
                allProjects = allProjects.Data,
                allSkill = allSkill.Data,
                allsoftSkill = allsoftSkill.Data,
                allTools = allTools.Data,
                allReward = allReward.Data,
                allCertify = allCertify.Data,
                profileCv,
                hidePhone = hideInfo,
                hideEmail = hideInfo
            };
            baseReult.Data = dataInfo;
            return baseReult;
        }
        public async Task<dynamic> GetDetailInfo(string searchId, int userId)
        {
            var data = await _repository.ExecuteSqlProcedure<SearchCVItemDisplay>(
                "sp_cv_getDeatailSearchId",
                new
                {
                    searchId
                }
            );
            var searchCVExtraModelInsert = new SearchCVExtraModel()
            {
                RelId = userId,
                CreateAt = DateTime.Now,
                SearchId = int.Parse(searchId),
                CreatedBy = userId,
                UpdatedBy = userId,
                Status = 1,
                Deleted = false,
                UpdateAt = DateTime.Now,
                TypeCount = 1,
            };
            var resultCheck = await _searchCVResultRepository.FindOneByStatementSql<SearchCVResultModel>(
                "select * from SearchResult where relId=  @searchId  and  CreatedBy = @userid",
                new
                {
                    searchId,
                    userid = userId
                }
            );
            if (resultCheck != null && resultCheck.Id > 0)
            {
                data.IsHideInfo = false;
            }
            else
            {
                data.IsHideInfo = true;
            }
            await _searchCVExtraRepository.AddOrUPdate(searchCVExtraModelInsert);
            var dataSearchCV = await _searchCVRepository.GetById(int.Parse(searchId));
            await _searchCVRepository.AddOrUPdate(dataSearchCV);
            var dataTemplate = data;
            return dataTemplate;
        }

    }
}
