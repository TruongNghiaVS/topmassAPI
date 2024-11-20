using Microsoft.AspNetCore.Mvc;

namespace Topmass.Image.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MediaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<MediaController> _logger;
        private readonly IFileMedia _fileMedia;

        public MediaController(ILogger<MediaController> logger,
            IWebHostEnvironment env,
            IFileMedia fileMedia)
        {
            _logger = logger;
            _env = env;
            _fileMedia = fileMedia;
        }

        [HttpPost]
        public async Task<ActionResult> UploadAvatar(
            IFormFile file = null
            )
        {
            var foldeUpload = "Avatar";
            var fileRequest = new FileResultRequest();
            fileRequest.Folder = foldeUpload;
            fileRequest.FileContent = file;
            var fileResult = await _fileMedia.UploadMedia(fileRequest);
            return StatusCode(fileResult.StatusCode, fileResult);
        }

        [HttpPost]
        public async Task<ActionResult> UploadBlog(
           IFormFile file = null
           )
        {
            var foldeUpload = "Blog";
            var fileRequest = new FileResultRequest();
            fileRequest.Folder = foldeUpload;
            fileRequest.FileContent = file;
            var fileResult = await _fileMedia.UploadMedia(fileRequest);
            return StatusCode(fileResult.StatusCode, fileResult);
        }



    }
}
