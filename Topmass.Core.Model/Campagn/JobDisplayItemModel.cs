namespace Topmass.Core.Model.Campagn
{
    public class JobDisplayItemModel : BaseModel
    {
        public int JobId { get; set; }

        public int RecurId { get; set; }

        public string JobName { get; set; }

        public string CompanyName { get; set; }

        public string RangeSalary { get; set; }

        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }

        public string ExperienceText { get; set; }

        public string LocationText { get; set; }

        public string LocationSearch { get; set; }

        public string Hashtags { get; set; }
        public string Slug { get; set; }
        public string ProfessionText { get; set; }

        public string TypeOfWork { get; set; }
        public string Rank { get; set; }

        public int RankSearch { get; set; }

        public DateTime? DateExpried { get; set; }




    }

}
