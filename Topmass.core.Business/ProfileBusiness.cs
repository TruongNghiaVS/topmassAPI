using Topmass.core.Business.Model;
using Topmass.Core.Model.Profile;
using Topmass.Core.Repository;


namespace Topmass.core.Business
{
    public partial class ProfileBusiness : IProfileBusiness
    {
        private readonly IEducationUserRepository _repository;

        private readonly IExperienceUserRepository _experienceUserRepository;

        private readonly IOtherProfileUserRepository _otherProfileUserRepository;

        private readonly IProjectUserRepository _projectUserRepository;

        private readonly ICertifyUserRepository _certifyUserRepository;

        private readonly IJobSettingUserRepository _jobSettingUserRepository;

        private readonly IProfileCVUserRepository _profileCVUserRepository;
        public ProfileBusiness(IEducationUserRepository userRepository,
            IExperienceUserRepository experienceUserRepository,
            IOtherProfileUserRepository otherProfileUserRepository,
            ICertifyUserRepository certifyUserRepository,
            IProjectUserRepository projectUserRepository,
            IJobSettingUserRepository jobSettingUserRepository,
            IProfileCVUserRepository profileCVUserRepository


            )
        {
            _repository = userRepository;
            _experienceUserRepository = experienceUserRepository;
            _otherProfileUserRepository = otherProfileUserRepository;
            _certifyUserRepository = certifyUserRepository;
            _projectUserRepository = projectUserRepository;
            _jobSettingUserRepository = jobSettingUserRepository;
            _profileCVUserRepository = profileCVUserRepository;
        }

        public async Task<EducationUserInfoReponse> AddEducation(EducationUserInfoRequest request)
        {
            var response = new EducationUserInfoReponse();

            var itemInsert = new EducationUser()
            {
                CreateAt = DateTime.Now,
                CreatedBy = request.UserId,
                Deleted = false,
                FromMonth = request.FromMonth,
                FromYear = request.FromYear,
                Introduction = request.Introduction,
                IsEnd = request.IsEnd,
                Major = request.Major,
                Position = request.Position,
                LinkFile = request.LinkFile,
                SchoolName = request.SchoolName,
                ToMonth = request.ToMonth,
                ToYear = request.ToYear,
                RelId = request.UserId,
                Status = 1,
                UpdateAt = DateTime.Now,
                UpdatedBy = request.UserId,
                Rank = request.Rank

            };
            await _repository.AddOrUPdate(itemInsert);
            await ReloadGenFileCV(request.UserId);
            return response;
        }


        public async Task<EducationUserInfoUpdateReponse> UpdateEducation(EducationUserInfoUpdateRequest request)
        {
            var response = new EducationUserInfoUpdateReponse();

            var itemupdate = await _repository.GetById(request.Id);
            itemupdate.FromMonth = request.FromMonth;
            itemupdate.FromYear = request.FromYear;
            itemupdate.Introduction = request.Introduction;
            itemupdate.IsEnd = request.IsEnd;
            itemupdate.Major = request.Major;
            itemupdate.Position = request.Position;
            itemupdate.LinkFile = request.LinkFile;
            itemupdate.SchoolName = request.SchoolName;
            itemupdate.ToMonth = request.ToMonth;
            itemupdate.ToYear = request.ToYear;

            itemupdate.UpdateAt = DateTime.Now;
            itemupdate.UpdatedBy = request.UserId;
            itemupdate.Rank = request.Rank;

            await _repository.AddOrUPdate(itemupdate);
            //await _repository.ExecuteSqlProcedure("sp_setReloadCV", new { userid = request.UserId });
            await ReloadGenFileCV(request.UserId);
            return response;
        }

        public async Task<bool> DeleteEducation
            (EducationUserInfoDeleteRequest request)
        {
            var itemDelete = await _repository.GetById(request.Id);

            return await _repository.Delete(itemDelete);

        }

        public async Task<EducationGetAllResult> GetAllEducation(int userId)
        {
            var data = await _repository.GetAllByStatementSql<EducationDisplayItem>(
                "select *, dbo.getNameMaster2(position) as  PositionText  from educationUser where relId = @userId order by id desc",
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



        public async Task<ExperienceUserInfoReponse> AddExperience(ExperienceUserInfoRequest request)
        {
            var response = new ExperienceUserInfoReponse();
            var itemInsert = new ExperienceUser()
            {
                CreateAt = DateTime.Now,
                CreatedBy = request.UserId,
                Deleted = false,
                FromMonth = request.FromMonth,
                FromYear = request.FromYear,
                Introduction = request.Introduction,
                IsEnd = request.IsEnd,
                Position = request.Position,
                LinkFile = request.LinkFile,
                CompanyName = request.CompanyName,
                ToMonth = request.ToMonth,
                ToYear = request.ToYear,
                RelId = request.UserId,
                Status = 1,
                UpdateAt = DateTime.Now,
                UpdatedBy = request.UserId,

            };
            await _experienceUserRepository.AddOrUPdate(itemInsert);
            await ReloadGenFileCV(request.UserId);

            return response;
        }


        public async Task<ExperienceUserInfoUpdateReponse> UpdateExperience(ExperienceUserInfoUpdateRequest request)
        {
            var response = new ExperienceUserInfoUpdateReponse();

            var itemupdate = await _experienceUserRepository.GetById(request.Id);

            itemupdate.FromMonth = request.FromMonth;
            itemupdate.FromYear = request.FromYear;
            itemupdate.Introduction = request.Introduction;
            itemupdate.IsEnd = request.IsEnd;

            itemupdate.Position = request.Position;
            itemupdate.LinkFile = request.LinkFile;
            itemupdate.CompanyName = request.CompanyName;
            itemupdate.ToMonth = request.ToMonth;
            itemupdate.ToYear = request.ToYear;

            itemupdate.UpdateAt = DateTime.Now;
            itemupdate.UpdatedBy = request.UserId;

            await _experienceUserRepository.AddOrUPdate(itemupdate);
            await ReloadGenFileCV(request.UserId);
            return response;
        }

        public async Task<bool> DeleteExperience
            (ExperienceUserInfoDeleteRequest request)
        {
            var itemDelete = await _experienceUserRepository.GetById(request.Id);
            return await _experienceUserRepository.Delete(itemDelete);

        }

        public async Task<ExperienceGetAllResult> GetAllExperience(int userId)
        {
            var data = await _experienceUserRepository.GetAllByStatementSql<ExperienceDisplayItem>(
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


        public async Task<OtherUserProfileGetAllResult> GetAllOtherProfileUser(int typeData, int userId)
        {
            if (typeData == 1)
            {
                var data1 = await _experienceUserRepository.GetAllByStatementSql<OtherUserProfileSkillDisplayItem>(
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
            var data = await _experienceUserRepository.GetAllByStatementSql<OtherUserProfileDisplayItem>(
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

        public async Task<List<Idinfo>> GetAllId(int userId, int type = 0)
        {
            var sqltext = "";
            if (type == 0)
            {
                sqltext = "select id from ProjectUser d where d.RelId =  @userId";
            }
            if (type == 1)
            {
                sqltext = "select id from educationUser d where d.RelId =  @userId";
            }
            if (type == 2)
            {
                sqltext = "select id from experienceUser d where d.RelId =  @userId";
            }

            if (type == 3)
            {
                sqltext = "select id from OtherProfileUser where  TypeData = 1 and CreatedBy = @userId";
            }
            if (type == 4)
            {
                sqltext = "select id from CertifyUser where  TypeData = 1 and RelId = @userId";
            }
            if (type == 5)
            {
                sqltext = "select id from OtherProfileUser where  TypeData = 2 and CreatedBy = @userId";
            }
            if (type == 6)
            {
                sqltext = "select id from OtherProfileUser where  TypeData = 3 and CreatedBy = @userId";
            }
            if (type == 10)
            {
                sqltext = "select id from CertifyUser where  TypeData = 2 and RelId = @userId";
            }
            return await _certifyUserRepository.GetAllByStatementSql<Idinfo>(sqltext,
                new { userId });

        }
        public async Task<Idinfo> GetInfomationCandidateByMail(string email)
        {
            var sqltext = "";
            if (string.IsNullOrEmpty(email))
            {
                return new Idinfo();
            }

            var result = await _certifyUserRepository.ExecuteSqlProcedure<Idinfo>("sp_getInformationCandidate",
                new { email = email });
            var data = result;
            if (data == null)
            {
                return new Idinfo();
            }
            return data;

        }

        public async Task<OtherProfileRequestReponse> AddOtherProfileUser(List<AddOtherProfileRequest> request)
        {
            var response = new OtherProfileRequestReponse();
            foreach (var item in request)
            {

                await AddOrUpdateProfileRequest(item);
            }

            return response;
        }
        public async Task<bool> DeleteOtherProfileUser(int id)
        {
            return await _otherProfileUserRepository.Delete(id);
        }
        private async Task<bool> AddOrUpdateProfileRequest(AddOtherProfileRequest request)
        {

            var itemInsert = new OtherProfileUser()
            {
                CreateAt = DateTime.Now,
                CreatedBy = request.UserId,
                Deleted = false,
                Status = 1,
                UpdateAt = DateTime.Now,
                UpdatedBy = request.UserId,
                Description = request.Description,
                FullName = request.FullName,
                TypeData = request.TypeData,
                Level = request.Level,
                Id = request.Id
            };
            return await _otherProfileUserRepository.AddOrUPdate(itemInsert);

        }

        public async Task<ProjectUserInfoReponse> AddProject(ProjectUserInfoRequest request)
        {
            var response = new ProjectUserInfoReponse();

            var itemInsert = new ProjectUser()
            {
                CreateAt = DateTime.Now,
                CustomerName = request.CustomerName,
                NumOfMember = request.NumOfMember,
                ProjectName = request.ProjectName,
                Techology = request.Techology,
                CreatedBy = request.UserId,
                Deleted = false,
                FromMonth = request.FromMonth,
                FromYear = request.FromYear,
                Introduction = request.Introduction,
                IsEnd = request.IsEnd,

                Position = request.Position,
                LinkFile = request.LinkFile,

                ToMonth = request.ToMonth,
                ToYear = request.ToYear,
                RelId = request.UserId,
                Status = 1,
                UpdateAt = DateTime.Now,
                UpdatedBy = request.UserId,


            };
            await _projectUserRepository.AddOrUPdate(itemInsert);
            return response;
        }


        public async Task<ProjectUserInfoUpdateReponse> UpdateProject(ProjectUserInfoUpdateRequest request)
        {
            var response = new ProjectUserInfoUpdateReponse();

            var itemupdate = await _projectUserRepository.GetById(request.Id);
            itemupdate.FromMonth = request.FromMonth;
            itemupdate.FromYear = request.FromYear;
            itemupdate.Introduction = request.Introduction;
            itemupdate.IsEnd = request.IsEnd;
            itemupdate.Position = request.Position;
            itemupdate.LinkFile = request.LinkFile;
            itemupdate.ToMonth = request.ToMonth;
            itemupdate.ToYear = request.ToYear;
            itemupdate.UpdateAt = DateTime.Now;
            itemupdate.UpdatedBy = request.UserId;
            itemupdate.ProjectName = request.ProjectName;
            itemupdate.CustomerName = request.CustomerName;
            itemupdate.NumOfMember = request.NumOfMember;
            itemupdate.Position = request.Position;
            itemupdate.Techology = request.Techology;
            await _projectUserRepository.AddOrUPdate(itemupdate);
            return response;
        }

        public async Task<bool> DeleteProjectUser
            (ProjectUserInfoDeleteRequest request)
        {
            var itemDelete = await _projectUserRepository.GetById(request.Id);
            return await _projectUserRepository.Delete(itemDelete);

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





        public async Task<CertifyUserInfoReponse> AddCertify(CertifyUserInfoRequest request)
        {
            var response = new CertifyUserInfoReponse();

            var itemInsert = new CertifyUser()
            {
                CreateAt = DateTime.Now,
                MonthGet = request.MonthGet,
                YearGet = request.YearGet,
                FullName = request.FullName,
                CompanyName = request.CompanyName,
                TypeData = request.TypeData,
                IsExpired = request.IsExpired,
                MonthExpired = request.MonthExpired,
                YearExpired = request.YearExpired,


                CreatedBy = request.UserId,
                Deleted = false,

                Introduction = request.Introduction,


                LinkFile = request.LinkFile,


                RelId = request.UserId,
                Status = 1,
                UpdateAt = DateTime.Now,
                UpdatedBy = request.UserId,


            };
            await _certifyUserRepository.AddOrUPdate(itemInsert);
            return response;
        }


        public async Task<CertifyUserInfoUpdateReponse> UpdateCertify(CertifyUserInfoUpdateRequest request)
        {
            var response = new CertifyUserInfoUpdateReponse();

            var itemupdate = await _certifyUserRepository.GetById(request.Id);
            itemupdate.MonthGet = request.MonthGet;
            itemupdate.YearGet = request.YearGet;
            itemupdate.Introduction = request.Introduction;
            itemupdate.CompanyName = request.CompanyName;
            itemupdate.FullName = request.FullName;
            itemupdate.LinkFile = request.LinkFile;
            itemupdate.UpdateAt = DateTime.Now;
            itemupdate.UpdatedBy = request.UserId;
            itemupdate.IsExpired = request.IsExpired;
            itemupdate.MonthExpired = request.MonthExpired;
            itemupdate.YearExpired = request.YearExpired;

            await _certifyUserRepository.AddOrUPdate(itemupdate);
            return response;
        }
        public async Task<bool> DeleteCertifyUser
            (CertifyUserInfoDeleteRequest request)
        {
            var itemDelete = await _certifyUserRepository.GetById(request.Id);
            return await _certifyUserRepository.Delete(itemDelete);

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


        public async Task<GetJobSettingReponse> GetJobSetting(int userId)
        {
            var data = await _repository.GetAllByStatementSql<JobSettingUser>(
                "select * from UserJobSetting where RelId = @RelId ",
                new
                {
                    RelId = userId
                }
             );

            var userJobSetting = data.FirstOrDefault();

            if (userJobSetting == null)
            {
                userJobSetting = new JobSettingUser() { };
            }
            var reponse = new GetJobSettingReponse()
            {
                Data = userJobSetting
            };

            return reponse;

        }

        public async Task<JobSettingReponse> SaveJobSetting(JobSettingRequest request)
        {
            var response = new JobSettingReponse();

            //var itemInsert = new JobSettingUser()
            //{
            //    CreateAt = DateTime.Now,
            //    CreatedBy = request.UserId,
            //    Deleted = false,
            //    RelId = request.UserId,
            //    Status = 1,
            //    UpdateAt = DateTime.Now,
            //    UpdatedBy = request.UserId,
            //    Experience = request.Experience,
            //    Field = request.Field,
            //    Gender = request.Gender,
            //    LocationAddress = request.LocationAddress,
            //    Position = request.Position,
            //    Salary = request.Salary,
            //    Skill = request.Skill,

            //};

            var itemDeCertify = await _certifyUserRepository.FindOneByStatementSql<JobSettingUser>("select * from UserJobSetting where RelId = @RelId", new
            {
                RelId = request.UserId
            });

            if (itemDeCertify == null)
            {
                itemDeCertify = new JobSettingUser();
                itemDeCertify.CreateAt = DateTime.Now;
                itemDeCertify.CreatedBy = request.UserId;
                itemDeCertify.Deleted = false;
                itemDeCertify.RelId = request.UserId;
                itemDeCertify.Status = 1;
                itemDeCertify.UpdateAt = DateTime.Now;
                itemDeCertify.UpdatedBy = request.UserId;
            }
            itemDeCertify.UpdateAt = DateTime.Now;
            itemDeCertify.UpdatedBy = request.UserId;
            itemDeCertify.Experience = request.Experience;
            itemDeCertify.Field = request.Field;
            itemDeCertify.Gender = request.Gender;
            itemDeCertify.LocationAddress = request.LocationAddress;
            itemDeCertify.Position = request.Position;
            itemDeCertify.Salary = request.Salary;
            itemDeCertify.Skill = request.Skill;

            response.Data = await _jobSettingUserRepository.AddOrUPdate(itemDeCertify);
            return response;
        }



        public async Task<bool> AddOrUpdateProfileCV(ProfileUserRequestAdd request)
        {
            var profileUser = await _profileCVUserRepository.FindOneByStatementSql<ProfileCVUser>("select * from ProfileCV where RelId = @userId",

                     new
                     {
                         userId = request.UserId
                     }
             );
            if (profileUser != null)
            {

                profileUser.UpdateAt = DateTime.Now;
                profileUser.UpdatedBy = request.UserId;
            }
            else
            {
                profileUser = new ProfileCVUser();
                profileUser.RelId = request.UserId;
                profileUser.CreateAt = DateTime.Now;
                profileUser.CreatedBy = request.UserId;
            }
            profileUser.AvatarLink = request.AvatarLink;
            profileUser.PhoneNumber = request.PhoneNumber;
            profileUser.Position = request.Position;
            profileUser.FullName = request.FullName;
            profileUser.Level = request.Level;
            profileUser.Gender = request.Gender;
            profileUser.Email = request.Email;
            profileUser.AddressInfo = request.AddressInfo;
            profileUser.Introduction = request.Introduction;
            profileUser.DateOfBirth = request.DateOfBirth;
            profileUser.ProvinceCode = request.ProvinceCode;
            await ReloadGenFileCV(request.UserId);
            return await _profileCVUserRepository.AddOrUPdate(profileUser);
        }


        public async Task<ProfileCVUserDisplay> GetProfileUserCV(int userId)
        {
            var profileUser = await _profileCVUserRepository.FindOneByStatementSql<ProfileCVUserDisplay>("select *, dbo.getprovinceName( ProvinceCode ) as ProvinceName  from ProfileCV where RelId = @userId",
                    new
                    {
                        userId = userId
                    }
            );
            if (profileUser != null)
            {

                return profileUser;
            }

            profileUser = new ProfileCVUserDisplay();
            return profileUser;
        }

        public async Task<dynamic> GetAlNTD(int userId)
        {
            var profileUser = await _profileCVUserRepository
                                    .ExecuteSqlProcerduceToList<NTDViewer>("sp_user_getallNTD",
            new
            {
                userId = userId
            }
            );
            if (profileUser == null)
            {
                return new List<NTDViewer>();
            }
            return profileUser;
        }
        public async Task<bool> ReloadGenFileCV(int userId)
        {
            await _profileCVUserRepository.ExecuteSqlProcedure("sp_setLoadNewFile", new
            {
                userid = userId
            });
            return true;

        }


        public async Task<List<RegionalSearchItem>> GetRegionSearchSetting(int userId)
        {
            var dataResult = await _profileCVUserRepository.ExecuteSqlProcerduceToList<RegionalSearchItem>("getRegionSearchSetting", new
            {
                userid = userId
            });
            return dataResult;

        }

    }
}
