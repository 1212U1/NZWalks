namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double LengthInKM { get; set; }
        public String WalkImageURL { get;set; }

        public Guid DifficultyID { get; set; }
        public Difficulty Difficulty { get; set; }

        public Guid RegionID { get; set; }
        public Region Region { get; set; }
    }
}
