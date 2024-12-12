namespace Topmass.Core.Model
{
    public class RecruiterModel : BaseModel
    {
        public string TaxCode { get; set; }
        public string Code { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateRegister { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? NumberLightning { get; set; }

        public RecruiterModel()
        {
            NumberLightning = 0;
        }


    }

    public class RecruiterModel2 : RecruiterModel
    {
        public int LevelAuthen { get; set; }
    }
    public class RecruiterInfoModel : BaseModel
    {
        public int? LevelAuthen { get; set; }

        public DateTime? DateActive { get; set; }

        public string? Email { get; set; }

        public int? RelId { get; set; }

        public string? AvatarLink { get; set; }

        public bool? Gender { get; set; }


        public RecruiterInfoModel()
        {

        }


    }

    public class RecruiterAuthen : BaseModel
    {

        public int? LevelAuthen { get; set; }

        public DateTime? DateActive { get; set; }

        public string? Content { get; set; }
    }



    public class CompanyInfoModel : BaseModel
    {
        public string? TaxCode { get; set; }

        public string? Website { get; set; }

        public string? Capacity { get; set; }
        public string? Email { get; set; }

        public string? EmailCompany { get; set; }

        public int? RelId { get; set; }

        public string? FullName { get; set; }


        public string? AddressInfo { get; set; }

        public string? Phone { get; set; }

        public string? shortDes { get; set; }
        public string? MapInfo { get; set; }
        public string? Slug { get; set; }
        public string? LogoLink { get; set; }
        public string? CoverLink { get; set; }

        public int? Field { get; set; }

        public string? IframeEmbeddedMap { get; set; }

        public CompanyInfoModel()
        {
            MapInfo = "";
            Field = -1;
        }

    }

    public class BusinessLicenseLogModel : BaseModel
    {

        public string? Noted { get; set; }

        public string? ExtraInfo { get; set; }

        public string? Email { get; set; }

        public int? RelId { get; set; }



    }


    public class BusinessLicenseModel : BaseModel
    {

        public string? Email { get; set; }
        public string? LinkFile { get; set; }
        public string? Note { get; set; }
        public string DocumnetType { get; set; }
        public int? RelId { get; set; }

        public int ReasonReject { get; set; }

    }

}