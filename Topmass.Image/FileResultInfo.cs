namespace Topmass.Image
{
    public class FileResultInfo
    {
        public string FileName { get; set; }

        public string FullLink { get; set; }

        public string ShortLink { get; set; }

    }
    public class FileInfo
    {
        public FileStream FileContent { get; set; }

    }
    public class FileResultRequest
    {
        public IFormFile FileContent { get; set; }
        public string? Folder { get; set; }

    }
}
