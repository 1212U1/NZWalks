using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalMediaUpload : IMediaUploadRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDBContext nZWalksDBContext;

        public LocalMediaUpload(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,
                                NZWalksDBContext nZWalksDBContext) 
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nZWalksDBContext = nZWalksDBContext;
        }
        public async Task<Media> UploadImageAsync(Media media)
        {
           String destinationPath= Path.Combine(this.webHostEnvironment.ContentRootPath,"Media",$"{ media.FileName}{ media.FileExtension}");
           using(Stream stream = new FileStream(destinationPath, FileMode.Create))
           {
                await media.File.CopyToAsync(stream);
           }
            //http:localhost:13845/Media/Upload.jpg
            String mediaURL = $"{this.httpContextAccessor.HttpContext.Request.Scheme}:{this.httpContextAccessor.HttpContext.Request.Host}{this.httpContextAccessor.HttpContext.Request.PathBase}/Media/{media.FileName}{media.FileExtension}";
            media.FilePath = mediaURL;
            await this.nZWalksDBContext.Media.AddAsync(media);
            await this.nZWalksDBContext.SaveChangesAsync();
            return media;
        }
    }
}
