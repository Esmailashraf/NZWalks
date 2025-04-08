using System.ComponentModel.DataAnnotations;

namespace NZwalks.api.Models.DTO
{
    public class registerRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string[] Roles { get; set; }
    }
}
