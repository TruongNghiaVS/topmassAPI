namespace Topmass.Recruiter.Model
{

    public class OpenSearchCVRequest
    {
        public int? SearchId { get; set; }


        public string? LinkFile { get; set; }
        public int? Campaign { get; set; }


    }

    public class SaveSearchCVRequest
    {
        public int SearchId { get; set; }
        public string LinkFile { get; set; }
        public int Campaign { get; set; }
        public int JobId { get; set; }
        public SaveSearchCVRequest()
        {
            Campaign = -1;
        }

    }


}
