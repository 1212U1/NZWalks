using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using Serilog;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaUploadController : ControllerBase
    {
        private readonly IMediaUploadRepository mediaUploadRepository;
        private readonly ILogger<MediaUploadController> logger;

        public MediaUploadController(IMediaUploadRepository mediaUploadRepository, ILogger<MediaUploadController> logger)
        {
            this.mediaUploadRepository = mediaUploadRepository;
            this.logger = logger;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadMediaAsync([FromForm]MediaUploadRequestDTO mediaUploadRequestDTO)
        {
            try
            {
                logger.LogInformation("Upload controller was invoked");
                throw new Exception("Custome xception");
                ValidateFileUpload(mediaUploadRequestDTO);
                if (ModelState.IsValid)
                {
                    Media media = new Media
                    {
                        File = mediaUploadRequestDTO.File,
                        FileDescription = mediaUploadRequestDTO.FileDescription,
                        FileExtension = Path.GetExtension(mediaUploadRequestDTO.File.FileName),
                        FileSizeInBytes = mediaUploadRequestDTO.File.Length,
                        FileName = mediaUploadRequestDTO.File.FileName,
                    };
                    media = await this.mediaUploadRepository.UploadImageAsync(media);
                    return Ok(media);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Custom exception");
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(MediaUploadRequestDTO mediaUploadRequestDTO)
        {
            List<String> allowedExtensions = new List<string> { ".jpg",".jpeg",".png"};
            if(!allowedExtensions.Contains(Path.GetExtension(mediaUploadRequestDTO.File.FileName)))
            {
                ModelState.AddModelError("file", "File extension not supported");
            }
            if (mediaUploadRequestDTO.File.Length > 10485760) 
            {
                ModelState.AddModelError("file", "File size not supported");

            }
        }
    }
}
