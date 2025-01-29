using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddDifficultyRequestDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
