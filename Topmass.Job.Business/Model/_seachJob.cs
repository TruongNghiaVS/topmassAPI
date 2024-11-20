namespace Topmass.Job.Business.Model
{
    public class GetAllBestJobOptimizationRequest
    {
        public int? Limit { get; set; }
        public int? Page { get; set; }
        public string? OrderBy { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? ValueType { get; set; }
        public string? TypeSearch { get; set; }

        public int? UserId { get; set; }

        public GetAllBestJobOptimizationRequest()
        {
            UserId = 0;
        }

    }

    public interface IBaseSearch
    {

        public int? Limit { get; set; }
        public int? Page { get; set; }
    }
    public class GetSuitableJobRequest : IBaseSearch
    {
        public string LocationSearch { get; set; }

        public int UserId { get; set; }

        public int? Limit { get; set; }
        public int? Page { get; set; }
        public GetSuitableJobRequest()
        {
            UserId = 0;
            Limit = 1;
            Page = 9;
        }
    }

    public class SearchJobRequest
    {
        public string? KeyWord { get; set; }

        public int? TypeOfWork { get; set; }

        public int? RankLevel { get; set; }

        public int? Location { get; set; }

        public int? Field { get; set; }

        public int? ModeGet { get; set; }

        public int UserId { get; set; }

        public int ModeOrderBy { get; set; }

        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public int? Gender { get; set; }
        public int ExperienceId { get; set; }
        public SearchJobRequest()
        {
            UserId = 0;
            ModeOrderBy = -1;
            SalaryFrom = -1;
            SalaryTo = -1;
            Gender = -1;
            ExperienceId = -1;
        }




    }

    public class GetAttractiveJobs : GetSuitableJobRequest
    {
        public int? UserId { get; set; }
        public GetAttractiveJobs()
        {
            UserId = -1;
        }
    }
    public class BaseReponse
    {
        public int TotalRecord { get; set; }

        public int? Page { get; set; }

        public int? Limit { get; set; }

    }
    public class GetAllBestJobOptimizationReponse : BaseReponse
    {

        public List<BestJobOptimizationDisplayItemData> Data { get; set; }

        public GetAllBestJobOptimizationReponse()
        {
            Data = new List<BestJobOptimizationDisplayItemData>();
        }


    }


}
