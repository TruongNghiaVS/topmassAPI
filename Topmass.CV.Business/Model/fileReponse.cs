namespace Topmass.CV.Business.Model
{


    public class FileReponse
    {

        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public DataFileInfo Data { get; set; }

        public FileReponse()
        {
        }

    }

    public class DataFileInfo
    {
        public string FileName { get; set; }
        public string FullLink { get; set; }
        public string ShortLink { get; set; }
    }
}

