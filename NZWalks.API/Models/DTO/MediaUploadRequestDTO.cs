using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.DTO
{
    public class MediaUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        public String?  FileDescription { get; set; }
        [Required]
        public String FileName { get; set; }
    }
}
