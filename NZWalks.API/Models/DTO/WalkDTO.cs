namespace NZWalks.API.Models.DTO
{
    public class WalkDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        //public Guid DifficultyId { get; set; }
        //public Guid RegionId { get; set; }


        //Navigating DTOs
        public RegionDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }
    }
}
