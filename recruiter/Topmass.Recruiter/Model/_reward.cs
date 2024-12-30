namespace Topmass.Recruiter.Model
{

    public class OpenSearchCVRequest
    {
        public int? SearchId { get; set; }
        public string? LinkFile { get; set; }
        public int? Campaign { get; set; }
    }
    //public Task<BaseResult> ExchangePointToOpenCVNoSearchCV(int
    // searchId, int point, int userId, int identify, string fileName);
    public class OpenSearchCVRequestNoSearch
    {
        public int SearchId { get; set; }
        public int Identify { get; set; }
        public string LinkFile { get; set; }

    }


    public class SaveSearchCVRequest
    {
        public int SearchId { get; set; }
        public string LinkFile { get; set; }
        public int Campaign { get; set; }
        public int JobId { get; set; }
        public int LockInfo { get; set; }
        public SaveSearchCVRequest()
        {
            Campaign = -1;
        }

    }

    public class CheckFileGenCVDegital
    {
        public int SearchId { get; set; }
        public bool? Lockfile { get; set; }
        public CheckFileGenCVDegital()
        {
            Lockfile = true;
        }

    }

    public class OpenViewerCVRequest
    {
        public int? ViewerId { get; set; }

    }

}
