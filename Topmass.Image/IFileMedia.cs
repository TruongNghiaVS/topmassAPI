using TopMass.Core.Result;

namespace Topmass.Image
{
    public interface IFileMedia
    {
        public Task<BaseResult> UploadMedia(FileResultRequest request);
        //public Task<FileInfo> GetFileMedia(string? shortLink);
    }
}
