namespace topmass.Model
{
    public class GetAllBestJobOptimization
    {

        public int? Limit { get; set; }
        public int? Page { get; set; }
        public string? OrderBy { get; set; }

        public string? ValueType { get; set; }

        public string? TypeSearch { get; set; }
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
        public GetAllBestJobOptimization()
        {
            Limit = 100;
            Page = 1;

        }
    }


    public class BestJobOptimizationDisplayItemData
    {
        public int JobId { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Salaryfrom { get; set; }
        public int SalaryTo { get; set; }
        public string RangeSalary { get; set; }
        public string PositionText { get; set; }
        public string JobSlug { get; set; }
        public string LocationText { get; set; }
        public string LogoImage { get; set; }
        public string CompanyLogo
        {
            get
            {
                if (string.IsNullOrEmpty(LogoImage))
                {
                    return string.Empty;
                }

                return "https://www.cdn.topmass.vn/static/" + LogoImage;


            }
        }



    }

    public class GetAttractiveJobsRequest
    {

        public int? Limit { get; set; }
        public int? Page { get; set; }
        public int? ModeGet { get; set; }
        public GetAttractiveJobsRequest()
        {
            Limit = 9;
            Page = 1;

        }
    }


    public class InputGetSuitableJobRequest
    {

        public int? Limit { get; set; }
        public int? Page { get; set; }


        public InputGetSuitableJobRequest()
        {
            Limit = 9;
            Page = 1;

        }
    }

    public class GetJobLastestRequest
    {

        public int? Limit { get; set; }
        public int? Page { get; set; }




        public GetJobLastestRequest()
        {
            Limit = 10;
            Page = 1;

        }
    }

    public class InputSearchJobsRequest
    {
        public int? Limit { get; set; }
        public int? Page { get; set; }
        public string? KeyWord { get; set; }

        public int? Gender { get; set; }
        public int? TypeOfWork { get; set; }
        public int? RankLevel { get; set; }
        public int? Location { get; set; }
        public int? Field { get; set; }
        public int? ModeGet { get; set; }

        public int SalaryFrom { get; set; }

        public int SalaryTo { get; set; }

        public int ExperienceId { get; set; }
        public int? ModeOrderBy { get; set; }
        public InputSearchJobsRequest()
        {
            Limit = 10;
            Page = 1;
            ModeOrderBy = -1;
            SalaryFrom = -1;
            SalaryTo = -1;
            ExperienceId = -1;
            Gender = -1;
        }
    }

}
