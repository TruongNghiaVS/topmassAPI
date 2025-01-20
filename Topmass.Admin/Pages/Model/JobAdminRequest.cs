namespace Topmass.Admin.Pages.Model
{
    public class UpdateJobRequest
    {
        public int Id { get; set; }
        public string TitleJob { get; set; }
        public string TimeWorking { get; set; }
        public string Description { get; set; }
        public string Requirment { get; set; }
        public string Benefit { get; set; }
        public int RangeSalaryType { get; set; }
        public int unitMoney { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
    }

}
