using Microsoft.AspNetCore.Mvc;

namespace Topmass.Service.Image.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {


        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<FileUploadController> _logger;
        public FileUploadController(
            ILogger<FileUploadController> logger,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpload(UploadFileAdd request)
        {
            var listEror = new List<object>();
            var fileRequest = request.FileRequest;
            if (string.IsNullOrEmpty(fileRequest.FileName))
            {
                var itemError = new
                {
                    name = "txtFileRequest",
                    Content = "Thiếu thông tin file "
                };
                listEror.Add(itemError);
            }
            if (listEror.Count > 0)
            {
                return new JsonResult(listEror)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (string.IsNullOrEmpty(request.Folder))
            {
                request.Folder = "temp";
            }
            var fileName = request.FileName;
            if (string.IsNullOrEmpty(request.FileName))
            {
                fileName = DateTime.Now.Second + "" + new Random().Next(100000);
            }
            var pathfolder = Path.Combine(_hostingEnvironment.WebRootPath, request.Folder);
            bool folderExists = Directory.Exists(pathfolder);
            if (!folderExists)
            {
                Directory.CreateDirectory(pathfolder);
            }
            var pathFile = Path.Combine(pathfolder, fileName);
            using (FileStream stream = new FileStream(pathFile, FileMode.Create))
            {
                await fileRequest.CopyToAsync(stream);
                stream.Close();
            }
            var linkResult = pathFile;
            var dataReponse = new
            {
                success = true,
                linkResult
            };
            return new JsonResult(dataReponse)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

    }
}
