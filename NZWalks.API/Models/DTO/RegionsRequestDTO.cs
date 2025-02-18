using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class RegionsRequestDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code should be minimum 3 characters")]
        [MaxLength(3,ErrorMessage ="Code should be maximum 3 characters")]
        public String Code { get; set; }
        [Required]
        public String Name { get; set; }
        public String? RegionImageURL { get; set; }
    }
}
