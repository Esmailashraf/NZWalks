using System.ComponentModel.DataAnnotations;

namespace NZwalks.api.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MaxLength(100,ErrorMessage ="name not across the limit")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage ="it mustn't less than 3")]
        [MaxLength(3,ErrorMessage ="it mustn't greater than 3")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
