using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateDifficultyRequestDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
