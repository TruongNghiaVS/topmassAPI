namespace Topmass.Recruiter.Bussiness
{
    public class RecruiterRegisterRequest : BaseUserRegisterRequest
    {

        public string? TaxCode { get; set; }


    }





    public class RecruiterInfoUpdate
    {
        public string Phone { get; set; }

        public string Name { get; set; }

        public int RecuruiterId { get; set; }

        public int HandleBy { get; set; }
        public string? Taxcode { get; set; }
        public string AvatarLink { get; set; }
        public string Email { get; set; }
        public int RelId { get; set; }
        public bool? Gender { get; set; }
        public RecruiterInfoUpdate()
        {
            HandleBy = -1;
        }

    }


    public class CompanyInfoRequestUpdate
    {
        public string? TaxCode { get; set; }

        public string? Website { get; set; }
        public int HandleBy { get; set; }
        public string? Capacity { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }

        public string? AddressInfo { get; set; }
        public string? Phone { get; set; }

        public string? ShortDes { get; set; }
        public string? LogoLink { get; set; }
        public string? CoverLink { get; set; }

        public string? EmailCompany { get; set; }
        public string? MapInfo { get; set; }
        public int? Field { get; set; }


        public CompanyInfoRequestUpdate()
        {
            HandleBy = -1;
            Field = -1;
        }

    }


    public class BusinessLicenseRequestUpdate
    {

        public int HandleBy { get; set; }
        public string Email { get; set; }
        public string? LinkFile { get; set; }
        public BusinessLicenseRequestUpdate()
        {
            HandleBy = -1;
        }

    }
}
