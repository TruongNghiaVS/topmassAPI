namespace Topmass.Recruiter.Bussiness.Model
{
    public class CompanyInfoItem
    {
        public string? TaxCode { get; set; }

        public string? Website { get; set; }

        public string? Email { get; set; }

        public string? Capacity { get; set; }

        public int? RelId { get; set; }

        public int? Field { get; set; }

        public string? FullName { get; set; }


        public string? AddressInfo { get; set; }

        public string? Phone { get; set; }

        public string? ShortDes { get; set; }
        public string? LogoLink { get; set; }
        public string? CoverLink { get; set; }
        public string? IframeEmbeddedMap
        {
            get; set;
        }
        public CompanyInfoItem()
        {
            TaxCode = "";
            Website = "";
            Capacity = "";
            Email = "";
            RelId = -1;
            FullName = "";
            AddressInfo = "";
            Phone = "";
            ShortDes = "";
            LogoLink = "";
            CoverLink = "";



        }

    }


    public class BusinessLicenseItem
    {
        public string LinkFile { get; set; }

        public int StatusCode { get; set; }

        public string StatusText { get; set; }
        public int ReasonReject { get; set; }

        public string DocumnetType { get; set; }

        public string ReasonRejectText
        {
            get
            {
                if (StatusCode != 2)
                {
                    return "";
                }
                if (ReasonReject == 0)
                {
                    return "Chứng từ không rõ";
                }
                if (ReasonReject == 1)
                {
                    return "Thông tin không trùng khớ";
                }
                if (ReasonReject == 2)
                {
                    return "Chứng từ bản cũ";
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
        public string Note { get; set; }

    }


}
