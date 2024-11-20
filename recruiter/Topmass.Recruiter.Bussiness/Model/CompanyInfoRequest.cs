namespace Topmass.Recruiter.Bussiness
{


    public class CompanyInfoResult
    {

    }



    public class CompanyInfoItemDisplay
    {

        public string? TaxCode { get; set; }

        public string? Website { get; set; }

        public string? Email { get; set; }

        public string? Capacity { get; set; }

        public int? RelId { get; set; }

        public string? FullName { get; set; }
        public string? AddressInfo { get; set; }

        public string? Phone { get; set; }

        public string? ShortDes { get; set; }
        public string? LogoLink { get; set; }
        public string? CoverLink { get; set; }
        public CompanyInfoItemDisplay()
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



}
