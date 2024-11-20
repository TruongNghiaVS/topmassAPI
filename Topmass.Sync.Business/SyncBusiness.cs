using Newtonsoft.Json;
using Topmass.core.Business;
using Topmass.Sync.Repository;
using Topmass.Utility;

namespace Topmass.Sync.Business
{
    public class SyncBusiness : ISyncBusiness
    {
        private readonly ISyncDaRepository _dataRepository;
        private readonly IProfileBusiness _profileBusiness;
        public SyncBusiness(ISyncDaRepository syncDaRepository,
            IProfileBusiness profileBusiness)
        {
            _dataRepository = syncDaRepository;
            _profileBusiness = profileBusiness;
        }
        public async Task<bool> HandleCVSyncDataFromTopmass()
        {
            var allCandidateNeedSync = await _dataRepository.GetAllIdCandidateNeedSyncs();
            if (allCandidateNeedSync.Count < 1)
            {
                return true;
            }
            foreach (var candidate in allCandidateNeedSync)
            {

                await HanldeOneCaseSync(candidate.Id);
            }
            return true;
        }

        private async Task<bool> HanldeOneCaseSync(int candidateId)
        {
            var educationText = "";
            var experienceText = "";
            var educationInfosData = await _profileBusiness.GetAllEducation(candidateId);
            if (educationInfosData.Data.Count > 0)
            {
                var dataEducation = educationInfosData.Data.OrderByDescending(x => x.ToYear);
                var itemData = dataEducation.FirstOrDefault();
                if (itemData != null)
                {
                    educationText = itemData.SchoolName;
                }
            }
            var expericesText = "Chưa có kinh nghiệm";

            var experienceContent = "";
            var expericesData = await _profileBusiness.GetAllExperience(candidateId);
            if (expericesData.Data.Count > 0)
            {
                var dataexperices = expericesData.Data.OrderByDescending(x => x.ToYear);
                var itemData = dataexperices.FirstOrDefault();
                if (itemData != null)
                {
                    var numberexper = int.Parse(itemData.ToYear) - int.Parse(itemData.FromYear);
                    if (numberexper == 0)
                    {
                        expericesText = "Chưa có kinh nghiệm";
                    }
                    else if (numberexper < 1)
                    {
                        expericesText = "Chưa có kinh nghiệm";
                    }
                    else if (numberexper < 3)
                    {
                        expericesText = "Kinh nghiệm từ 1 đến 3 năm";
                    }
                    else if (numberexper < 5)
                    {
                        expericesText = "Kinh nghiệm Từ 3 đến 5 năm";
                    }
                    else
                    {
                        expericesText = "Trên 5 năm";
                    }
                    experienceContent = itemData.CompanyName;
                }
            }
            var dataProfile = await _profileBusiness.GetProfileUserCV(candidateId);
            var gender = -1;
            var dobYear = -1;
            var position = "";
            var introdution = "";
            if (dataProfile.Id > 0)
            {
                introdution = dataProfile.Introduction;
                position = dataProfile.Position;
                dobYear = dataProfile.DateOfBirth.HasValue ? dataProfile.DateOfBirth.Value.Year : -1;
                gender = dataProfile.Gender == 0 ? 1 : 2;
            }


            var educations = educationInfosData.Data;
            var experiences = expericesData.Data;
            var allProjects = await _profileBusiness.GetAllProjectUser(candidateId);
            var allSkill = await _profileBusiness.GetAllOtherProfileUser(1, candidateId);
            var allsoftSkill = await _profileBusiness.GetAllOtherProfileUser(2, candidateId);
            var allTools = await _profileBusiness.GetAllOtherProfileUser(3, candidateId);
            var allReward = await _profileBusiness.GetAllCertifyUser(candidateId, 2);
            var allCertify = await _profileBusiness.GetAllCertifyUser(candidateId, 1);
            var profileCv = await _profileBusiness.GetProfileUserCV(candidateId);
            var dataInfo = new
            {
                educations = educations,
                experiences = experiences,
                allProjects = allProjects.Data,
                allSkill = allSkill.Data,
                allsoftSkill = allsoftSkill.Data,
                allTools = allTools.Data,
                allReward = allReward.Data,
                allCertify = allCertify.Data,
                profileCv
            };
            var content = JsonConvert.SerializeObject(dataInfo);
            var content2 = Utilities.RemoveSign4VietnameseString(content);
            var locationCode = 79;
            var locationText = "Hồ Chí Minh";

            var rankEducation = 0;
            var rankEducationtemp = Utilities.SlugifySlug(educationText.ToLower());
            if (rankEducationtemp.Contains("cao-dang"))
            {
                rankEducation = 3;
            }
            else if (rankEducationtemp.Contains("dai-hoc"))
            {
                rankEducation = 4;
            }
            else if (rankEducationtemp.Contains("trung-cap"))
            {
                rankEducation = 1;
            }
            else if (rankEducationtemp.Contains("thpt"))
            {
                rankEducation = 2;
            }
            else
            {

            }
            var requestInsert = new
            {
                phone = profileCv.PhoneNumber,
                fullName = profileCv.FullName,
                email = profileCv.Email,
                position,
                LocationText = locationText,
                IntroductionText = profileCv.Introduction,
                LocationCode = locationCode,
                gender,
                dobYear,

                randEducation = rankEducation,
                contentCV = content,
                contentCV2 = content2,
                linkCV = "",
                linkCVHide = "",
                expericesText = expericesText,
                educationText = educationText,
                candidateId = candidateId,
                experienceContent

            };
            await _dataRepository.AddCVToSeachCV(requestInsert);
            return true;

        }
    }
}
