using Microsoft.AspNetCore.Mvc;
using Topmass.Image.Model;

namespace Topmass.Image.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileMediaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<FileMediaController> _logger;
        private readonly IFileMedia _fileMedia;

        public FileMediaController(ILogger<FileMediaController> logger,
            IWebHostEnvironment env,
            IFileMedia fileMedia)
        {
            _logger = logger;
            _env = env;
            _fileMedia = fileMedia;
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(
           [FromForm] FileRequest request
            )
        {
            var foldeUpload = "CV";
            var fileRequest = new FileUploadResultRequest();
            fileRequest.Folder = foldeUpload;
            fileRequest.FileContent = request.File;
            fileRequest.FileName = request.FileName;
            var fileResult = await _fileMedia.UploadFile(fileRequest);
            return StatusCode(fileResult.StatusCode, fileResult);
        }

    }
}
