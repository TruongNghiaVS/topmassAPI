namespace Topmass.Core.Model.Profile
{
    public class CertifyUser : BaseModel
    {
        public int TypeData { get; set; }


        public string? FullName { get; set; }

        public string? CompanyName { get; set; }

        public int MonthGet { get; set; }

        public int YearGet { get; set; }

        public string Introduction { get; set; }

        public string LinkFile { get; set; }


        public string? MonthExpired { get; set; }
        public string? YearExpired { get; set; }
        public bool? IsExpired { get; set; }


        public int RelId { get; set; }

        public CertifyUser()
        {
            MonthExpired = "0";
            YearExpired = "0";
            IsExpired = false;
        }

    }

}
