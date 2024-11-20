namespace Topmass.Core.Repository.IndexModel
{
    public class SearchRepJobRequest
    {

        public int UserId { get; set; }

        public int CampagnId { get; set; }

        public int? Status { get; set; }



        public int Page { get; set; }

        public int Limit { get; set; }


        public SearchRepJobRequest()
        {
            Status = -1;
            Page = 1;
            Limit = 10;

        }

    }

    public class SearchRepJobReponse
    {
        public int Page { get; set; }

        public int Limit { get; set; }

        public int TotalRecord
        {
            get
            {
                if (Data == null || Data.Count < 1)
                {
                    return 0;
                }

                return Data.Count;
            }
        }
        public List<JobItemIndex> Data { get; set; }



        public SearchRepJobReponse()
        {
            Page = 1;
            Limit = 10;

        }

    }

    public class JobItemIndex
    {
        public string Name { get; set; }
        public int? CampaignId { get; set; }
        public string CampaignName { get; set; }

        public int RelId { get; set; }

        public int? Status { get; set; }


        public DateTime CreateAt { get; set; }
        public int Reason { get; set; }


        public string PackageName { get; set; }
        public DateTime AuthorName { get; set; }

        public int ResultCode { get; set; }
        public string ResultText { get; set; }
        public int Id { get; set; }
        public int RuleStatusJob { get; set; }


        public DateTime? ExpiryDate { get; set; }
        public string DisplaySTatusText
        {
            get

            {
                if (RuleStatusJob != 2)
                {
                    return "Không hiển thị";
                }
                if (ExpiryDate.HasValue)
                {

                    if (ExpiryDate.Value.AddDays(1).Date <= DateTime.Now)
                    {
                        return "Hết hạn hiển thị";
                    }
                }
                if (Status == 0)
                {
                    return "Đang tắt";
                }
                return "Đang hiển thị";
            }

        }
        public string ReasonText
        {
            get

            {
                if (RuleStatusJob == 0)
                {
                    return "Đang xét duyệt";
                }
                else if (RuleStatusJob == 1)
                {
                    return "Đang xét duyệt";
                }
                else if (RuleStatusJob == 2)
                {
                    return "Đã duyệt";
                }
                else if (RuleStatusJob == 3)
                {
                    return "Bị từ chối";
                }
                else if (RuleStatusJob == 4)
                {
                    return "Tin bị khóa";
                }
                return "Chưa rõ lý do";
            }
        }

        public JobItemIndex()
        {

            PackageName = "Đăng tin miễn phí";

            Reason = 1;

        }

    }

    public class SearchRepJobLogView
    {
        public int JobId { get; set; }

        public int CampagnId { get; set; }

        public int Limit { get; set; }

        public int Page { get; set; }

        public int UserId { get; set; }
        public SearchRepJobLogView()
        {


        }

    }

    public class SearchRepJobLogViewReponse
    {
        public int Limit { get; set; }

        public int Page { get; set; }
        public List<JobLogViewIndexModel> Data { get; set; }
    }

}
