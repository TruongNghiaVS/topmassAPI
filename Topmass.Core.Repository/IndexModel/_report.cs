namespace Topmass.Core.Repository.IndexModel
{
    public class JobOverViewCounterRequest
    {
        public DateTime? From { get; set; }


        public DateTime? To { get; set; }

        public int JobId { get; set; }



        public JobOverViewCounterRequest()
        {


        }

    }


    public class JobOverViewCounterReponse
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int JobId { get; set; }

        public string JobName { get; set; }

        public int TotalViewer { get; set; }

        public int TotalApply { get; set; }

        public string StatusText { get; set; }

        public int Status { get; set; }

        public List<JobOverViewCounterDisplay> Data { get; set; }
        public JobOverViewCounterReponse()
        {
        }

    }


    public class JobOverViewCounterDisplay
    {
        public DateTime DayReport { get; set; }

        public int TotalViewer { get; set; }
        public int TotalApply { get; set; }



    }


    public class JobAppplyViewStatus
    {

        public int Id { get; set; }
        public int Status { get; set; }

    }


    public class JobOverviewDisplay
    {
        public int JobId { get; set; }

        public string JobName { get; set; }

        public int RuleStatus { get; set; }


        public string StatusText
        {
            get
            {

                if (Expired_date.HasValue == false)
                {
                    return "Hết hạn";
                }
                if (Expired_date.Value.AddDays(1).Date <= DateTime.Now.Date)
                {
                    return "Hết hạn";
                }
                if (Status == 2)
                {
                    return "Hết hạn";
                }

                if (RuleStatus == 2)
                {

                    if (Status == 1)
                    {
                        return "Đang chạy";
                    }
                    else
                    {
                        return "Không hiển thị";
                    }

                }
                if (RuleStatus == 0)
                {
                    return "Đang xét duyệt";
                }
                else if (RuleStatus == 1)
                {
                    return "Đang xét duyệt";
                }
                else if (RuleStatus == 3)
                {
                    return "Bị từ chối";
                }
                else if (RuleStatus == 4)
                {
                    return "Tin bị khóa";
                }
                return "Chưa rõ lý do";

            }
        }
        protected int Status { get; set; }


        public int StatusCode
        {
            get
            {
                if (StatusText == "Hết hạn")
                {
                    return 3;
                }
                else if (StatusText == "Đang chạy")
                {
                    return 2;
                }
                return 1;
            }
        }

        public DateTime? Expired_date { get; set; }

        public JobOverviewDisplay()
        {

        }
    }

}
