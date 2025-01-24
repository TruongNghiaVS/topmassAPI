namespace Topmass.Recruiter.Bussiness.Model
{
    public class RecruiterInfoRequest
    {
        public string Email { get; set; }
        public int RecuruiterId { get; set; }

    }

    public class RecruiterInfoUpdate
    {
        public string? Name { get; set; }
        public int? Gender { get; set; }
        public int? HandleBy { get; set; }
        public string AvatarLink { get; set; }
        public int? IdUpdate { get; set; }

    }

    public class DocumentBusinessGetInfo
    {
        public int DocumentStatusCode { get; set; }
    }


    public class RecruiterInfoResult
    {


        public string? Phone { get; set; }

        public string? Name { get; set; }

        public string RecruiterCode { get; set; }
        public int? Gender { get; set; }
        public bool? IsBlock { get; set; }
        public string? Email { get; set; }
        public int? Level { get; set; }
        //public DateTime? DateActive { get; set; }
        public int? Status { get; set; }

        public string? StatusText
        {
            get
            {
                return GetStatusText();
            }
        }

        public string? AvatarLink { get; set; }
        public string AuthenticationLevelText
        {
            get
            {
                return GetLevelText();
            }
        }



        private string GetStatusText()
        {
            if (Status == 0)
            {
                return "Đang chờ xác thực";
            }
            if (Status == 1)
            {
                return "Hoạt động";
            }
            if (Status == 2)
            {
                return "Đang bị khoá";
            }
            return "Không hoạt động";
        }

        private string GetLevelText()
        {

            if (Level == 0)
            {
                return "Chưa có xác thực mail";
            }
            if (Level == 1)
            {
                return "Cấp 1/3";
            }
            if (Level == 2)
            {
                return "Cấp 2/3";
            }

            if (Level == 3)
            {
                return "Cấp 3/3";
            }
            return "Chưa có xác thực mail";
        }

        public CompanyInfoItem CompanyInfo { get; set; }
        public BusinessLicenseItem BusinessLicenseInfo { get; set; }


        public int? NumberLightning { get; set; }




        public RecruiterInfoResult()
        {

            CompanyInfo = new CompanyInfoItem();
            BusinessLicenseInfo = new BusinessLicenseItem();
        }
    }
}
