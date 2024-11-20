namespace Topmass.Admin.Business
{
    public class SearchNTDRequest : BaseRequest
    {
        public int OrderBy { get; set; }
        public int CbDocumnetStatus { get; set; }

        public SearchNTDRequest()
        {
              
        }
    }

    public class SearchJobAdminRequest : BaseRequest
    {
        public int OrderBy { get; set; }
        public int CbDisplay { get; set; }
        public int CbStatus { get; set; }
        public int CbCompany { get; set; }
        public SearchJobAdminRequest()
        {

        }
    }
    public class UpdateJobAdmin
    {
        public int Id { get; set; }
        public int StatusChange { get; set; }
        public string NotedChange { get; set; }
    }
    public class UpdateDocumnetRequest
    {
        public int Id { get; set; }
        public string StatusChange { get; set; }
        public string NotedChange { get; set; }
        public string LinkFile { get; set; }
        public int ReasonReject { get; set; }
        public UpdateDocumnetRequest()
        {

        }
    }
    public class BaseRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Limit { get; set; }

        public int Page { get; set; }

        public string Token { get; set; }

        public int Status { get; set; }

        public int AuthenLevel { get; set; }

        public BaseRequest()
        {
            Limit = 20;
            Page = 1;
            Status = -1;
            From = DateTime.Now.AddDays(-30);
            To = DateTime.Now.AddDays(1);
            AuthenLevel = -1;
        }
    }

    public class SearchArticleRequest : BaseRequest
    {

    }
    public class TimeWorking
    {

        public string? Day_from { get; set; }

        public string? Day_to { get; set; }

        public string? Time_to { get; set; }

        public string? Time_from { get; set; }
        public TimeWorking()
        {
            Day_from = "";
            Day_to = "";
            Time_to = "";
            Time_from = "";
        }

    }


    public class LocationsJob
    {
        public string Location { get; set; }

        public string? LocationText { get; set; }
        public List<DistrictJob> Districts { get; set; }

    }


    public class DistrictJob
    {

        public string District { get; set; }

        public string? DistrictText { get; set; }

        public string Detail_location { get; set; }

        public DistrictJob()
        {
            District = "";
            Detail_location = "";
        }
    }


}
