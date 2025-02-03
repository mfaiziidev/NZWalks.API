namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        public int DifficultyId { get; set; }
        public int RegionId { get; set; }


        // Navigation Properties

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }


    }
}
