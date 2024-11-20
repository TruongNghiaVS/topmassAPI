namespace topmass.Model.Jobsave
{
    public class JobSaveDisplayItem
    {

        public string PositionText { get; set; }

        public string CompanyName { get; set; }

        public DateTime? BusinessDate { get; set; }

        public string LogoImage { get; set; }
        public int SalaryFrom { get; set; }

        public int SalaryTo { get; set; }

        public string FieldArray { get; set; }

        public int Id { get; set; }

        public int JobId { get; set; }






        public DateTime? LastUpdate { get; set; }

        public string LocationText { get; set; }
        public string JobSlug { get; set; }

        public JobSaveDisplayItem()
        {
            LocationText = "TP. HCM";
        }

    }
}
