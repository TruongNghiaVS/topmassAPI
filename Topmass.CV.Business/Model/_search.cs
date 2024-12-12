namespace Topmass.CV.Business.Model
{


    public class SearchCVRequestInfo
    {
        public string? KeyWord { get; set; }
        public string? LocationCode { get; set; }

        public string? CvKey { get; set; }

        public int Gender { get; set; }

        public DateTime? DayOfBirth { get; set; }

        public string? EducationalLevelArray { get; set; }

        public string? SchoolSearch { get; set; }

        public int? Limit { get; set; }

        public int? Page { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public SearchCVRequestInfo()
        {
            Limit = 10;
            Page = 1;
        }
    }


    public class SearchCVReponse
    {
        public List<ItemSearchCVDisplay> Data { get; set; }

        public int? Page { get; set; }

        public int Total { get; set; }

        public int? Limit { get; set; }
        public SearchCVReponse()
        {
            Limit = 10;
            Page = 1;

        }

    }

    public class ItemSearchCVDisplay
    {
        public int TotalRecord { get; set; }
        public string ExperienceText { get; set; }
        public string ExperienceContent { get; set; }
        public string EducationText { get; set; }
        public string JobObjectiveText { get; set; }
        public string Location { get; set; }
        public string LocationCode { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Position { get; set; }
        public string SalaryText { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public int TotalView { get; set; }
        public int TotalContact { get; set; }
        public string SearchId { get; set; }
        public string StatusProfile { get; set; }
        public string FullName { get; set; }
        public string Avatarlink { get; set; }
        public int SourceType { get; set; }
        public ItemSearchCVDisplay()
        {
            TotalContact = 0;
            TotalView = 0;
            SalaryFrom = 0;
            SalaryTo = 0;
            Position = "";
            SalaryText = "";
            LocationCode = "";
            Location = "";
            JobObjectiveText = "";
            SalaryText = "Thoả thuận";
            StatusProfile = "Đang tìm việc";
            ExperienceText = "";
            EducationText = "";
            SearchId = "-1";
            FullName = "Nguyễn Trường Nghĩa";
            Avatarlink = "";

        }

    }


}
