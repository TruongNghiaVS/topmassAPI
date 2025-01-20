using Topmass.Core.Model.Campagn;

namespace Topmass.Admin.Repository
{

    public class BasicInfoJobAdmin
    {
        public string Title { get; set; }
        public string Position { get; set; }
        public int RuleStatus { get; set; }
        public string CurrentRuleStatus { get; set; }

        public int Status { get; set; }

        public string DisplaySTatusText
        {
            get

            {
                if (RuleStatus != 2)
                {
                    return "Đang tắt";
                }
                if (ExpiryDate.HasValue)
                {

                    if (ExpiryDate.Value.AddDays(1).Date <= DateTime.Now)
                    {
                        return "Hết hạn";
                    }
                }
                if (Status == 0)
                {
                    return "Đang tắt";
                }
                return "Đang hiển thị";
            }

        }
        public string RuleStatusText
        {
            get

            {
                if (RuleStatus == 0)
                {
                    return "Đang xét duyệt";
                }
                else if (RuleStatus == 1)
                {
                    return "Đang xét duyệt";
                }
                else if (RuleStatus == 2)
                {
                    return "Đã duyệt";
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


        public string CampagnName { get; set; }

        public string CompanyName { get; set; }

        public DateTime? BusinessTime { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string Location { get; set; }

        public string LocationText { get; set; }

        protected DateTime CreateAt { get; set; }
        protected DateTime UpdateAt { get; set; }
        public string DateTimeCreateText
        {
            get
            {
                return CreateAt.ToString("dd/MM/yyyy");


            }
        }

        public string UpdateAtText
        {
            get
            {
                try
                {
                    var datetimeCal = CreateAt > UpdateAt ? CreateAt : UpdateAt;
                    return datetimeCal.ToString("dd/MM/yyyy HH:mm");
                }
                catch (Exception)
                {

                    return CreateAt.ToString("dd/MM/yyyy");
                }




            }
        }

        public string ExpiryDateText
        {
            get
            {
                if (ExpiryDate.HasValue)
                {
                    return ExpiryDate.Value.ToString("dd/MM/yyyy");
                }
                return string.Empty;
            }
        }

        public string BussinessDateText
        {
            get
            {
                if (BusinessTime.HasValue)
                {
                    BusinessTime.Value.ToString("dd/MM/yyyy");
                }
                return string.Empty;
            }
        }
    }


    public class DataInfoJobAdmin : JobInfoModel
    {

        public int AggrementGet
        {
            get
            {
                if (Aggrement.HasValue)
                {
                    if (Aggrement == true)
                        return 1;
                }
                return 0;
            }
        }


    }
}
