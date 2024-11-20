using TopMass.Core.Result;

namespace Topmass.Image
{
    public class FileMedia : IFileMedia
    {
        private readonly IWebHostEnvironment _env;
        public FileMedia(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<BaseResult> UploadMedia(FileResultRequest request)
        {
            var reponse = new BaseResult();
            if (request.FileContent == null)
            {
                reponse.AddError("file", "Không có thông tin file");
                return reponse;
            }


            var uploadDirecotroy = request.Folder;
            var folderroot = _env.ContentRootPath;
            var uploadPath = Path.Combine(folderroot, "uploads",
                uploadDirecotroy);
            var file = request.FileContent;

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            var fileName = DateTime.Now.Ticks + "" + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);
            using (var strem = File.Create(filePath))
            {
                file.CopyTo(strem);
            }

            var shortlink = Path.Combine(uploadDirecotroy, fileName
             );

            shortlink = shortlink.Replace("\\", "/");


            var fileInfo = new FileResultInfo()
            {
                FileName = fileName,

                FullLink = "https://www.cdn.topmass.vn/static/" + shortlink,

                ShortLink = shortlink
            };
            reponse.Data = fileInfo;
            return reponse;
        }




        public async Task<byte[]> GetFileMedia(string? shortLink)
        {
            var folderroot = _env.ContentRootPath;
            var shortlink = Path.Combine(folderroot, shortLink);

            byte[] fileBytes = System.IO.File.ReadAllBytes(shortlink);
            string fileName = DateTime.Now.Ticks + Path.GetExtension(shortlink);
            return fileBytes;

        }


    }
}
