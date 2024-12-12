using Newtonsoft.Json;
using Topmass.Business.Regional;
using Topmass.core.Business;
using Topmass.Sync.Repository;
using Topmass.Utility;

namespace Topmass.Sync.Business
{
    public class SyncBusiness : ISyncBusiness
    {
        private readonly ISyncDaRepository _dataRepository;
        private readonly IProfileBusiness _profileBusiness;
        private readonly IRegionalBusiness _regionalBusiness;
        private GlobalRegional globalRegional { get; set; }
        public SyncBusiness(ISyncDaRepository syncDaRepository,
            IProfileBusiness profileBusiness,
            IRegionalBusiness regionalBusiness
            )
        {
            _dataRepository = syncDaRepository;
            _profileBusiness = profileBusiness;
            _regionalBusiness = regionalBusiness;
        }
        public async Task<bool> HandleCVSyncDataFromTopmass()
        {
            var allCandidateNeedSync = await _dataRepository.GetAllIdCandidateNeedSyncs();

            var runJob = await _dataRepository.ExecuteSqlProcedure("sp_runJoblocationHasJob", new { });
            if (allCandidateNeedSync.Count < 1)
            {
                return true;
            }
            globalRegional = GlobalRegional.GetRegional();
            if (globalRegional.DataGlobal == null || globalRegional.DataGlobal.Count < 1)
            {
                var result2 = await _regionalBusiness.LoadAllData();
            }
            foreach (var candidate in allCandidateNeedSync)
            {
                await HanldeOneCaseSync(candidate.Id);
            }
            return true;


        }
        private int getEducationCode(int rankCode)
        {
            var result = 0;
            //var rankEducationtemp = Utilities.SlugifySlug(educationText.ToLower());
            if (rankCode == 88)
            {
                result = 3;
            }
            else if (rankCode == 89)
            {
                result = 4;
            }
            else if (rankCode == 87)
            {
                result = 1;
            }
            else if (rankCode == 86)
            {
                result = 2;
            }
            else
            {
                result = 5;
            }
            return result;
        }
        private async Task<bool> HanldeOneCaseSync(int candidateId)
        {
            var educationText = "";
            var experienceText = "";
            var educationInfosData = await _profileBusiness.GetAllEducation(candidateId);
            var randEducationInfomation = 0;
            if (educationInfosData.Data.Count > 0)
            {
                var dataEducation = educationInfosData.Data.OrderByDescending(x => x.ToYear);
                var itemData = dataEducation.FirstOrDefault();
                randEducationInfomation = educationInfosData.Data.Max(x => int.Parse(x.Position));
                if (itemData != null)
                {
                    educationText = itemData.SchoolName;

                }
            }
            var expericesText = "Chưa có kinh nghiệm";
            var experienceContent = "";
            var expericesData = await _profileBusiness.GetAllExperience(candidateId);
            var locationCode = "79";
            var locationText = "Hồ Chí Minh";
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
                if (string.IsNullOrEmpty(dataProfile.ProvinceCode))
                {
                    locationCode = "79";
                }

                else
                {
                    locationCode = dataProfile.ProvinceCode;
                }
                var resultLocation = globalRegional.GetRegionalById(locationCode);
                locationText = resultLocation.Name;
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
                educations,
                experiences,
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
                randEducation = getEducationCode(randEducationInfomation),
                contentCV = content,
                contentCV2 = content2,
                linkCV = "",
                linkCVHide = "",
                expericesText,
                educationText,
                candidateId,
                experienceContent
            };
            await _dataRepository.AddCVToSeachCV(requestInsert);
            return true;
        }
    }
}
