using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        [Required, Range(0, 50)]
        public string? WalkImageURL { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
