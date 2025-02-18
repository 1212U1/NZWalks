using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalksDTO
    {
        [Required]
        [MaxLength(255)]
        public String Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public String Description { get; set; }
        [Required]
        [Range(0,50)]
        public double LengthInKM { get; set; }
        public String WalkImageURL { get; set; }
        [Required]
        public Guid DifficultyID { get; set; }
        [Required]
        public Guid RegionID { get; set; }
    }
}
