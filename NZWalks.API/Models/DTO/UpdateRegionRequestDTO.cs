﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum required length is 3 chracters")]
        [MaxLength(3, ErrorMessage = "Maximum required length is 3 chracters")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
