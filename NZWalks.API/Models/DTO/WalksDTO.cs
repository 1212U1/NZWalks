using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class WalksDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double LengthInKM { get; set; }
        public String WalkImageURL { get; set; }

        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
