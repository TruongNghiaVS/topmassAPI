namespace Topmass.CV.Business.Model
{
    public class SearchCVItemDisplay
    {
        public string? FullName { get; set; }
        public string? DayOfBirth { get; set; }

        public string GenderText
        {
            get
            {
                if (Gender == 0)
                {
                    return "Nam";
                }
                else if (Gender == 1)
                {
                    return "Nữ";
                }
                return "Chưa cập nhật";

            }
        }
        public string? LocationText { get; set; }
        public string? ExperienceText { get; set; }
        public string? LevelText { get; set; }
        public string? IndustryText
        {
            get; set;
        }
        protected int? Gender { get; set; }
        public bool? WorkTypeText { get; set; }
        public int? SalaryFrom { get; set; }
        public int? SalaryTo { get; set; }
        public string? SalaryExpertText { get; set; }

        public DateTime? LastAccess { get; set; }


        public string LastUpdateText
        {
            get
            {
                if (LastAccess.HasValue)
                {
                    return LastAccess.Value.ToString("dd/MM/yyyy");

                }
                else
                {
                    return string.Empty;
                }


            }
        }

        public int Point { get; set; }
        public bool IsHideInfo { get; set; }

        protected string LinkCV { get; set; }
        protected string LinkCVHide { get; set; }

        protected string LinkFileCV
        {
            get
            {
                if (string.IsNullOrEmpty(LinkCV))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + LinkCV.Replace("\\", "/");

            }
        }

        public string CVLink
        {
            get
            {

                if (IsHideInfo == true)
                {
                    return LinkFileCVHide;
                }
                else
                {
                    return LinkFileCV;
                }
            }
        }

        protected string LinkFileCVHide
        {
            get
            {
                if (string.IsNullOrEmpty(LinkCVHide))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + LinkCVHide.Replace("\\", "/");

            }
        }
        public int SourceType { get; set; }
        public SearchCVItemDisplay()
        {
            FullName = string.Empty;
            DayOfBirth = string.Empty;
            LocationText = string.Empty;
            ExperienceText = string.Empty;
            IndustryText = "";
            LevelText = string.Empty;
            SalaryFrom = 0;
            SalaryTo = 0;
            IsHideInfo = true;
            SalaryExpertText = "Thoả thuận";
            LastAccess = DateTime.Now.AddDays(-2);
            Point = 2;
        }


    }
}
