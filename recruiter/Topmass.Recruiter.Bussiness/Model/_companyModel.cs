namespace Topmass.Recruiter.Bussiness.Model
{
    public class CompanyInfoItem
    {
        public string? TaxCode { get; set; }

        public string? Website { get; set; }

        public string? Email { get; set; }

        public string? Capacity { get; set; }

        public int? RelId { get; set; }

        public int? Field { get; set; }

        public string? FullName { get; set; }


        public string? AddressInfo { get; set; }

        public string? Phone { get; set; }

        public string? ShortDes { get; set; }
        public string? LogoLink { get; set; }
        public string? CoverLink { get; set; }
        public CompanyInfoItem()
        {
            TaxCode = "";
            Website = "";
            Capacity = "";
            Email = "";
            RelId = -1;
            FullName = "";
            AddressInfo = "";
            Phone = "";
            ShortDes = "";
            LogoLink = "";
            CoverLink = "";



        }

    }


    public class BusinessLicenseItem
    {
        public string LinkFile { get; set; }

        public int StatusCode { get; set; }

        public string StatusText { get; set; }

        public string Note { get; set; }

    }


}
