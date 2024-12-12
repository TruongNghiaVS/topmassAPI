using Topmass.core.Business.Model;

namespace Topmass.core.Business
{
    public partial interface IProfileBusiness
    {
        public Task<List<Idinfo>> GetAllId(int userId, int type = 0);
        public Task<Idinfo> GetInfomationCandidateByMail(string email);
        public Task<EducationUserInfoReponse> AddEducation(EducationUserInfoRequest request);
        public Task<EducationUserInfoUpdateReponse> UpdateEducation(EducationUserInfoUpdateRequest request);
        public Task<bool> DeleteEducation(EducationUserInfoDeleteRequest request);
        public Task<EducationGetAllResult> GetAllEducation(int userId);

        public Task<ExperienceUserInfoReponse> AddExperience(ExperienceUserInfoRequest request);
        public Task<ExperienceUserInfoUpdateReponse> UpdateExperience(ExperienceUserInfoUpdateRequest request);
        public Task<bool> DeleteExperience(ExperienceUserInfoDeleteRequest request);
        public Task<ExperienceGetAllResult> GetAllExperience(int userId);


        public Task<OtherProfileRequestReponse> AddOtherProfileUser(List<AddOtherProfileRequest> request);
        public Task<bool> DeleteOtherProfileUser(int id);

        public Task<OtherUserProfileGetAllResult> GetAllOtherProfileUser(int typeData, int userId);


        public Task<ProjectUserInfoReponse> AddProject(ProjectUserInfoRequest request);
        public Task<ProjectUserInfoUpdateReponse> UpdateProject(ProjectUserInfoUpdateRequest request);
        public Task<bool> DeleteProjectUser(ProjectUserInfoDeleteRequest request);
        public Task<ProjectUserGetAllResult> GetAllProjectUser(int userId);
        public Task<CertifyUserInfoReponse> AddCertify(CertifyUserInfoRequest request);
        public Task<CertifyUserInfoUpdateReponse> UpdateCertify(CertifyUserInfoUpdateRequest request);
        public Task<bool> DeleteCertifyUser(CertifyUserInfoDeleteRequest request);
        public Task<CertifyUserGetAllResult> GetAllCertifyUser(int userId, int typedata);


        public Task<JobSettingReponse> SaveJobSetting(JobSettingRequest request);

        public Task<GetJobSettingReponse> GetJobSetting(int userId);
        public Task<bool> AddOrUpdateProfileCV(ProfileUserRequestAdd request);
        public Task<ProfileCVUserDisplay> GetProfileUserCV(int userId);
        public Task<bool> ReloadGenFileCV(int userId);
        public Task<dynamic> GetAlNTD(int userId);
        public Task<List<RegionalSearchItem>> GetRegionSearchSetting(int userId);
    }
}
