namespace Topmass.Recruiter.Model
{
    public class ErrorItem
    {
        public string? Key { get; set; }
        public string? Message { get; set; }
    }

    public class ErrorData
    {

        public bool? Success { get; set; }

        public string? MessageError { get; set; }

        public List<ErrorItem> Data { get; set; }

        public ErrorData()
        {
            Data = new List<ErrorItem>();
        }

        public void AddError(string key = "", string message = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            Data.Add(new ErrorItem { Key = key, Message = message });
        }
        public bool HasErorr()
        {
            if (Data == null)
            {
                return false;
            }
            return Data.Count > 0;
        }

    }
}
