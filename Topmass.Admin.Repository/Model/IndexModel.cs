using Topmass.Core.Model.Campagn;

namespace Topmass.Admin.Repository
{
    public class ArtileIndexModel
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public string CoverImage { get; set; }

        public string ShortDes { get; set; }

        public string AuthorPost { get; set; }

        public string KeyWord { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public string Linked { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }


    }


    public class NTDItemDisplay
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Taxcode { get; set; }
        public DateTime? DateRegister { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }

        public string StatusText
        {
            get
            {
                //if (Status == 0)
                //{
                //    return "Khóa tài khoản";
                //}

                if (Status == 1)
                {
                    return "Đang Hoạt động";
                }

                //if (Status == 2)
                //{
                //    return "Khóa vĩnh viễn";
                //}
                //if (Status == 3)
                //{
                //    return "Đang pendding";
                //}
                return "Khóa tài khoản";
            }
        }

        protected int StatusDocumnet { get; set; }

        public string StatusDocumnetText
        {
            get
            {
                if (StatusDocumnet == 0)
                {
                    return "Chưa ghi nhận thông tin";
                }
                else if (StatusDocumnet == 1)
                {
                    return "Chờ duyệt";
                }
                else if (StatusDocumnet == 2)
                {
                    return "Từ chối";
                }
                else if (StatusDocumnet == 3)
                {
                    return "Đã duyệt";
                }
                return "Chưa ghi nhận thông tin";
            }
        }

        public DateTime? CreateAt { get; set; }

        public string CompanyName { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string AuthenText { get; set; }
    }



    public class JobAdminItemDisplay : JobInfoModel
    {

        public int? CampaignId { get; set; }
        public string CampaignName { get; set; }

        public string Position { get; set; }

        public string CompanyName { get; set; }
        public string DisplayStatus { get; set; }

        public DateTime? Expired_date { get; set; }
        public string DisplaySTatusText
        {
            get

            {
                //if (RuleStatus != 2)
                //{
                //    return "Đang tắt";
                //}
                if (Expired_date.HasValue)
                {

                    if (Expired_date.Value.AddDays(1).Date <= DateTime.Now)
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
        public int RuleStatus { get; set; }
        public int RelId { get; set; }
        public int Reason { get; set; }
        public string ReasonText { get; set; }
        public string PackageName { get; set; }
        public DateTime AuthorName { get; set; }
        public int ResultCode { get; set; }
        public string ResultText { get; set; }
        public string Expired_dateText
        {
            get
            {
                if (Expired_date.HasValue)
                {
                    return Expired_date.Value.ToString("dd/MM/yyyy");
                }
                return string.Empty;
            }
        }



        public string DatetimeUpdateText
        {
            get

            {
                var datetimeGet = this.UpdateAt > this.CreateAt ? this.UpdateAt : this.CreateAt;
                return datetimeGet.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }

    public class JobLogAdminItemDisplay
    {

        public int? RelId { get; set; }
        public int DataType { get; set; }

        public int Status { get; set; }

        public string Noted { get; set; }
        public DateTime CreateAt { get; set; }
        public string DataTypeText
        {
            get

            {
                if (DataType == 0)
                {
                    return "Thay đổi thông tin duyệt";
                }

                return "Thay đổi thông tin hiển thị";
            }

        }

        public string StatusText
        {
            get
            {

                if (DataType == 0)
                {
                    if (Status == 0)
                    {
                        return "Tắt hiển thị";

                    }
                    return "Bật hiển thị";
                }
                else
                {
                    if (Status == 0)
                    {
                        return "Đang xét duyệt";
                    }
                    else if (Status == 1)
                    {
                        return "Đang xét duyệt";
                    }
                    else if (Status == 2)
                    {
                        return "Đã duyệt";
                    }
                    else if (Status == 3)
                    {
                        return "Bị từ chối";
                    }
                    else if (Status == 4)
                    {
                        return "Tin bị khóa";
                    }

                }

                return "";


            }
        }

        public string DateTimeCreateText
        {
            get
            {
                return CreateAt.ToString("dd/MM/yyyy");


            }
        }
    }

    public class SearchNTDReponse
    {

        public List<NTDItemDisplay> Data { get; set; }

        public SearchNTDReponse()
        {
            Data = new List<NTDItemDisplay>();
        }
    }

    public class NTDShortInfo
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }

    public class NTDLogInfo
    {
        public int Id { get; set; }

        public string Noted { get; set; }

        public int Status { get; set; }

        protected string LinkFile { get; set; }

        public string FullLink
        {
            get
            {
                if (string.IsNullOrEmpty(LinkFile))
                {

                    return "javascript:void(0)";
                }
                if (LinkFile.Contains("Avatar"))
                {
                    return "https://www.cdn.topmass.vn/static/" + LinkFile;
                }
                return LinkFile;
            }

        }
        public int ReasonReject { get; set; }


        public string ReasonRejectText
        {
            get
            {
                if(Status !=2)
                {
                    return "";
                }    

                if (ReasonReject == 0)
                {
                    return "Chứng từ không rõ";
                }
                if (ReasonReject == 1)
                {
                    return "Thông tin không trùng khớp";
                }
                if (ReasonReject == 2)
                {
                    return "Chứng từ bản cũ ";
                }

                if (ReasonReject == 3)
                {
                    return "Chứng từ thiếu thông tin";
                }
                if (ReasonReject == 4)
                {
                    return "Chứng từ không hợp lệ";
                }
                if (ReasonReject == 5)
                {
                    return "Chứng từ không đúng";
                }
                if (ReasonReject == 6)
                {
                    return "Chứng từ quá hạn công chứng";
                }
                if (ReasonReject == 7)
                {
                    return "Khác";
                }
                return "";
            }
        }
        public string StatusText
        {

            get
            {

                if (Status == 3)
                {
                    return "Đã duyệt";
                }
                if (Status == 1)
                {
                    return "Chờ duyệt";
                }
                if (Status == 2)
                {
                    return "Từ chối";
                }
                return "Chưa ghi nhận thông tin";
            }
        }

        protected DateTime CreateAt { get; set; }

        public string DateTimeCreateText
        {
            get
            {
                return CreateAt.ToString("dd/MM/yyyy");


            }
        }
    }

    public class NTDAccountLogInfo
    {
        public int Id { get; set; }

        public string  NotedReason { get; set; }

        public string Noted { get; set; }

        public int RelId { get; set; }

        public int OldStatus { get; set; }

        

        public int NewStatus { get; set; }

        public string OldStatusText { get
            {
                if(OldStatus ==0)
                {
                    return "Khóa tài khoản";
                }
                if (OldStatus == 1)
                {
                    return "Hoạt động";
                }
                if (OldStatus == 2)
                {
                    return "Khoá vĩnh viễn";
                }
                if (OldStatus == 3)
                {
                    return "Đang pendding";
                }
                return "Khóa tài khoản";

            }
        }

        public string NewStatusText
        {
            
            get
            {
                if (NewStatus == OldStatus)
                {
                    return "-";
                }    
                if (NewStatus == 0)
                {
                    return "Khóa tài khoản";
                }
                if (NewStatus == 1)
                {
                    return "Hoạt động";
                }
                if (NewStatus == 2)
                {
                    return "Khoá vĩnh viễn";
                }
                if (NewStatus == 3)
                {
                    return "Đang pendding";
                }
                return "Khóa tài khoản";

            }
        }
        public int OldConfirmStatus { get; set; }
        public int NewConfirmStatus { get; set; }

        public string OldConfirmStatusText
        {
            get
            {
                if (OldConfirmStatus == 0)
                {
                    return "Chưa xác thực email";
                }
                if (OldConfirmStatus == 1)
                {
                    return "Cấp độ 1";
                }
                if (OldConfirmStatus == 2)
                {
                    return "Cấp độ 2";
                }
                if (OldConfirmStatus == 3)
                {
                    return "Cấp độ 3";
                }
                return "Chưa xác thực email";

            }
        }

        public string NewConfirmStatusText
        {
            get
            {
                if(NewConfirmStatus == OldConfirmStatus)
                {
                    return "-";
                }    
                if (NewConfirmStatus == 0)
                {
                    return "Chưa xác thực email";
                }
                if (NewConfirmStatus == 1)
                {
                    return "Cấp độ 1";
                }
                if (NewConfirmStatus == 2)
                {
                    return "Cấp độ 2";
                }
                if (NewConfirmStatus == 3)
                {
                    return "Cấp độ 3";
                }
                return "Chưa xác thực email";

            }
        }
        public DateTime CreateAt { get; set; }
        public string DateTimeCreateText
        {
            get
            {
                return CreateAt.ToString("dd/MM/yyyy");
            }
        }
    }

    public class BasicInfoNTD
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public string UserName { get; set; }

        protected DateTime DateCreate { get; set; }

        public string DateTimeCreateText
        {
            get
            {
                return DateCreate.ToString("dd/MM/yyyy");


            }
        }

        public int LevelAuthen1 { get; set; }

        public int StatusCompany { get; set; }

        public string LevelLevelAuthen1Text
        {
            get
            {

                if (LevelAuthen1 == 0)
                {
                    return "Chưa xác thực email";
                }
                else if (LevelAuthen1 == 1)
                {
                    return "Cấp độ 1";
                }
                else if (LevelAuthen1 == 2)
                {
                    return "Cấp độ 2";
                }
                else if (LevelAuthen1 == 3)
                {
                    return "Cấp độ 3";
                }
                return "Chưa xác thực email";
            }
        }

    }



    public class CompanyInfoNTD
    {
        public string Name { get; set; }
        public string TaxCode { get; set; }
        public string Website { get; set; }
        public string Capacity { get; set; }
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Field { get; set; }

        public string AddressInfo { get; set; }

        public string Phone { get; set; }
        public string ShortLink { get; set; }

        public string LogoLink { get; set; }

        public string CoverLink { get; set; }

        public string FullLinkCoverLink
        {

            get
            {
                if (string.IsNullOrEmpty(CoverLink))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + CoverLink;
            }
        }

        public string FullLinkLogoLink
        {

            get
            {
                if (string.IsNullOrEmpty(LogoLink))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + LogoLink;
            }
        }
        public string EmailCompany { get; set; }

        public string Slug { get; set; }


        public string MapInfo { get; set; }


        public string ShortDes { get; set; }


    }

    public class DocumentNTDInfo
    {
        public string LinkFile { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }



    }
    public class SearchJobAdminReponse
    {
        public List<JobAdminItemDisplay> Data { get; set; }
        public SearchJobAdminReponse()
        {
            Data = new List<JobAdminItemDisplay>();
        }
    }
}
