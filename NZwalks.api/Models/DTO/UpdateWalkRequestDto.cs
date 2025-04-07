﻿using System.ComponentModel.DataAnnotations;

namespace NZwalks.api.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "name not across the limit")]

        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "name not across the limit")]

        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]

        public Guid DifficultyId { get; set; }
        [Required]

        public Guid RegionId { get; set; }
    }
}
