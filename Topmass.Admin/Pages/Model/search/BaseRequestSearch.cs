namespace Topmass.Admin.Pages.Model.search
{
    public class BaseInputRequestSearch : IInputBaseRequestSearch
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Limit { get; set; }

        public int Page { get; set; }

        public string Status { get; set; }
        public string Token { get; set; }

        public string FromText
        {
            get
            {

                return From.ToString("yyyy-MM-dd");

            }
        }
        public string ToText
        {
            get
            {

                return To.ToString("yyyy-MM-dd");
            }
        }
        public BaseInputRequestSearch()
        {
            Limit = 20;
            Page = 1;
            From = DateTime.Now.AddDays(-30);
            To = DateTime.Now.AddDays(1);

        }
    }

    public class NTDRequest : BaseInputRequestSearch
    {

        
        public int CbStatus { get; set; }
        public int CbDocumnetStatus { get; set; }
        public int Orderby { get; set; }
        public string AuthenLevel
        {
            get; set;
        }

        public NTDRequest()
        {
            AuthenLevel = "-1";
            CbStatus = -1;
            CbDocumnetStatus = -1;
            Orderby = 0;
            From = DateTime.Now.AddMonths(-3).Date;
            To = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
        }
    }



    public class JobInputRequest : BaseInputRequestSearch
    {


        public int CbDisplay { get; set; }
        public int CbStatus { get; set; }
        public int Orderby { get; set; }

        public int CbCompany { get; set; }
        public string AuthenLevel
        {
            get; set;
        }

        public JobInputRequest()
        {
            AuthenLevel = "-1";
            CbStatus = -1;
            CbDisplay = -1;
            Orderby = 0;
            CbCompany = -1;
            From = DateTime.Now.AddMonths(-3).Date;
            To = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
        }
    }


}
