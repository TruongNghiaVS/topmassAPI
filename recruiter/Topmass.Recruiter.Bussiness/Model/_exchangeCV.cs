using Topmass.Core.Model.Reward;

namespace Topmass.Recruiter.Bussiness.Model
{
    public class ExchangeCVRequestAdd
    {
        public string Title { get; set; }
        public int Point { get; set; }
        public DateTime? BusinessDate { get; set; }
        public int UserId { get; set; }
        public string Position { get; set; }
        public string Rank { get; set; }
        public string Experience { get; set; }
        public List<LinkFileExchangeCV> LinkCVs { get; set; }
        public ExchangeCVRequestAdd()
        {
            Point = 0;
            BusinessDate = DateTime.Now;
            Experience = string.Empty;
            Rank = string.Empty;
        }
    }

    public class LinkFileExchangeCV
    {
        public string LinkFile { get; set; }

    }

    public class ExchangeCVDesiplay : ExchangeCV
    {
        public string RankName { get; set; }

        public string ExperienceName { get; set; }
    }

    public class ExchangeCVDetailDisplay
    {
        public string Linkfile { get; set; }

        public int Status { get; set; }

        public string StatusName
        {
            get
            {
                if (Status == 0)
                {
                    return "Đang chờ duyệt";
                }
                else if (Status == 1)
                {

                    return "Xác thực";
                }
                else if (Status == 2)
                {
                    return "Từ chối";
                }

                return "Huỷ";
            }
        }
        public string Noted { get; set; }

    }

    public class ExchangeCVDisplay
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Point { get; set; }
        public string Position { get; set; }
        public string Rank { get; set; }
        public string Experience { get; set; }
        public DateTime BusinessTime { get; set; }

        public int Status { get; set; }
        public string StatusName
        {
            get
            {
                if (Status == 0)
                {
                    return "Đang chờ duyệt";
                }
                else if (Status == 1)
                {

                    return "Xác thực";
                }
                else if (Status == 2)
                {
                    return "Từ chối";
                }
                return "Huỷ";
            }
        }
        public ExchangeCVDisplay()
        {
            Position = "";
            Rank = "";
            Experience = "";
        }
    }

}

