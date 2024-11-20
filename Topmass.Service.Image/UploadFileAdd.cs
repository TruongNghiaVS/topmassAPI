namespace Topmass.Service.Image
{
    public class UploadFileAdd
    {
        public IFormFile FileRequest { get; set; }
        public string? Folder { get; set; }

        public string? FileName { get; set; }
    }

    public class UploadFileReult
    {
        public string? FullLink { get; set; }
        public string? ShortLink { get; set; }
    }
}
