

using Topmass.Core.Repository.IndexModel;

namespace Topmass.Job.Business.Model
{

    public class JobItemBusinessUpdate : JobItemBusinessAdd
    {

        public int JobId { get; set; }


    }

    public class JobItemBusinessAdd
    {
        public int HandleBy { get; set; }
        public string? Name { get; set; }
        public int? Campaign { get; set; }

        public string? Position { get; set; }
        public int? Profession { get; set; }
        public DateTime? Expired_date { get; set; }
        public int? Quantity { get; set; }
        public int? Type_of_work { get; set; }
        public int? Rank { get; set; }

        public int? Experience { get; set; }

        public bool? Aggrement { get; set; }

        public int? Salary_from { get; set; }

        public int? Salary_to { get; set; }

        public string? Type_money { get; set; }


        public int? Gender { get; set; }

        public string? Description { get; set; }

        public string? Requirement { get; set; }


        public string? Benefit { get; set; }



        public string? Username { get; set; }

        public string? Phone { get; set; }

        public List<EmailProper>? Emails { get; set; }

        public List<SkillProper> Skills { get; set; }
        public List<TimeWorking>? Time_working { get; set; }

        public List<LocationsJob>? Locations { get; set; }


    }


    public class EmailProper
    {

        public string Email { get; set; } = string.Empty;
    }


    public class SkillProper
    {
        public string Skill { get; set; } = string.Empty;


    }
    public class TimeWorking
    {

        public string? Day_from { get; set; }

        public string? Day_to { get; set; }

        public string? Time_to { get; set; }

        public string? Time_from { get; set; }
        public TimeWorking()
        {
            Day_from = "";
            Day_to = "";
            Time_to = "";
            Time_from = "";
        }

    }


    public class LocationsJob
    {
        public string Location { get; set; }

        public string? LocationText { get; set; }
        public List<DistrictJob> Districts { get; set; }

    }


    public class DistrictJob
    {

        public string District { get; set; }

        public string? DistrictText { get; set; }

        public string Detail_location { get; set; }

        public DistrictJob()
        {
            District = "";
            Detail_location = "";
        }
    }



    public class JobItemUpdate
    {
        public string Name { get; set; }
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
        public int Package { get; set; }
        public int HandleBy { get; set; }
        public int JobId { get; set; }



    }

    public class JobItemStatusUpdate
    {

        public int? IdUpdate { get; set; }
        public int? Status { get; set; }

        public int HandleBy { get; set; }



    }


    public class JobSearchRequest
    {
        public int? Userid { get; set; }

        public int Limit { get; set; }

        public int Page { get; set; }

        public string? Email { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }


        public int? CampagnId { get; set; }
        public int? Status { get; set; }

        public JobSearchRequest()
        {
            Limit = 10;
            Page = 1;
        }

    }

    public class GetAllVierOfJobRequest
    {
        public int JobId { get; set; }

        public int Limit { get; set; }

        public int Page { get; set; }
        public int CampagnId { get; set; }

        public int HandleId { get; set; }
        public GetAllVierOfJobRequest()
        {
            Limit = 10;
            Page = 1;
        }

    }

    public class JobInfoRequest
    {
        public int CampagnId { get; set; }

        public int JobId { get; set; }

        public int HandleBy { get; set; }

        public string Slug { get; set; }
    }

    public class ViewJobUserAddRequest
    {
        public string jobslug { get; set; }

        public int Userid { get; set; }


    }


    public class GetOverViewByJobId
    {
        public int JobId { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
        public GetOverViewByJobId()
        {


        }

    }


    public class JobInfoOverView
    {

        public string JobName { get; set; }
        public string ServiceName { get; set; }
        public string StatusText { get; set; }
        public int TotalViewer { get; set; }
        public int TotalCVApply { get; set; }
        public int Rate { get; set; }


    }

    public class ReportStaticInfoOverviewItem
    {

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int JobId { get; set; }

        public string JobName { get; set; }

        public int Rate { get; set; }

        public string ServiceName { get; set; }

        public string StatusText { get; set; }

        public int StatusCode { get; set; }



        public int TotalViewer { get; set; }

        public int TotalApply { get; set; }

        public List<JobOverViewCounterDisplay> Data { get; set; }
        public ReportStaticInfoOverviewItem()
        {

            ServiceName = "Miễn phí";

        }

    }

    public class ReponseGetOverViewByJobId
    {



        public JobInfoOverView OverView { get; set; }

        public List<ReportStaticInfoOverviewItem> DataReport { get; set; }
        public ReponseGetOverViewByJobId()
        {


        }

    }



    public class CandidateJobInfoRequest
    {
        public int JobId { get; set; }

        public string Slug { get; set; }

        public int UserId { get; set; }

    }



    public class JobInfoDisplay
    {

        public string JobName { get; set; }

        public string RangeSalary { get; set; }

        public string LocationText { get; set; }

        public string ExperienceText { get; set; }

        public string Hashtags { get; set; }

        public string Content { get; set; }

        public string Benefit { get; set; }

        public string Requirement { get; set; }

        public string Description { get; set; }

        public string Skill { get; set; }
        public string Slug { get; set; }
        public DateTime? Expired_date { get; set; }
        public JobCommonData CommonData { get; set; }

        public bool? Aggrement { get; set; }
        public string CurrencyCode { get; set; }


        public int SalaryFrom { get; set; }

        public int SalaryTo { get; set; }

        public string? Locations { get; set; }
        public string? Time_workings { get; set; }


        public List<LocationsJob> LocationsInfoMation { get; set; }
        public JobInfoDisplay()
        {
            JobName = "";
            RangeSalary = "";
            LocationText = "";
            ExperienceText = "";
            Hashtags = "";
            Benefit = "";
            Requirement = "";
            Description = "";
            Expired_date = null;
            LocationsInfoMation = new List<LocationsJob>();
        }
    }

    public class CompanyInfoDisplay
    {
        public string CompanyName { get; set; }

        public string Capacity { get; set; }

        public string AddressInfo { get; set; }

        public int CompanyId { get; set; }

        public string Slug { get; set; }

        public string AvatarLink { get; set; }


        public CompanyInfoDisplay()
        {
            AddressInfo = "";
            AvatarLink = "";
            CompanyId = -1;
            CompanyName = "";
            Capacity = "";
            Slug = "";
        }
    }


    public class JobCommonData
    {

        public DateTime? PublishDate { get; set; }

        public string ExperienceText { get; set; }

        public string LevelText { get; set; }

        public int NumOfRecruits { get; set; }

        public string ProfessionText { get; set; }

        public string FormOfWork { get; set; }

        public string FieldText { get; set; }

        public string GenderText { get; set; }



        public JobCommonData()
        {
            PublishDate = null;

            ExperienceText = "";
            LevelText = "";
            NumOfRecruits = -1;
            ProfessionText = "";
            FormOfWork = "";
            FieldText = "";
            GenderText = "";
        }





    }


    public class JobDetailResult
    {

        public int JobId { get; set; }

        public CompanyInfoDisplay CompanyData { get; set; }


        public JobInfoDisplay DataJob { get; set; }

        public dynamic JobExtra { get; set; }

        public JobDetailResult()
        {
            CompanyData = new CompanyInfoDisplay();
            DataJob = new JobInfoDisplay();
            JobExtra = new
            {
                isAply = true,
                isSave = true
            };
        }
    }



    public class JobRelattionRequest
    {
        public string Slug { get; set; }

        public int UserId { get; set; }

    }


    public class JobRelattionReponse
    {

        public List<JobRelationItemDisplay> Data { get; set; }
    }


    public class JobRecommendedRequest
    {
        public string Slug { get; set; }

        public int UserId { get; set; }

    }


    public class JobRecommendedReponse
    {

        public List<JobrecommendedItemDisplay> Data { get; set; }
    }



    public class JobItemAdd
    {

    }


    public class GetInfoForEditReponse

    {
        public DateTime? UpdateAt { get; set; }


        public int Status { get; set; }

        public int Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public int CreatedBy { get; set; }


        public string? Name { get; set; }
        public int? Campaign { get; set; }

        public string? Position { get; set; }
        public int? Profession { get; set; }
        public DateTime? Expired_date { get; set; }
        public int? Quantity { get; set; }
        public int? Type_of_work { get; set; }
        public int? Rank { get; set; }

        public int? Experience { get; set; }

        public bool? Aggrement { get; set; }

        public int? Salary_from { get; set; }

        public int? Salary_to { get; set; }

        public string? Type_money { get; set; }

        public int? Gender { get; set; }

        public string? Description { get; set; }

        public string? Requirement { get; set; }


        public string? Benefit { get; set; }

        public string? Skill { get; set; }

        public string? Username { get; set; }

        public string? Phone { get; set; }

        public int? RuleStatus { get; set; }
        public List<LocationsJob> Locations { get; set; }
        public List<EmailProper> Emails { get; set; }
        public List<TimeWorking> TimeWorks { get; set; }

        public List<SkillProper> Skills { get; set; }




    }


}