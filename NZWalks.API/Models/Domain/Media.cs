using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Media
    {
        public Guid ID { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileDescription { get; set; }
        public String FileExtension { get; set; }
        public String FileName { get; set; }
        public String FilePath { get; set; }
        public Int64 FileSizeInBytes { get; set; }
    }
}
