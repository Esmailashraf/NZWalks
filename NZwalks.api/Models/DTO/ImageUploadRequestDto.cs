using System.ComponentModel.DataAnnotations;

namespace NZwalks.api.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }

        public string? Description { get; set; }
    }
}
