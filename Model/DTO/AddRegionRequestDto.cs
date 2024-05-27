using System.ComponentModel.DataAnnotations;

namespace NZWalk.API.Model.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(6, ErrorMessage ="Code has to be minimum of 6 digits")]
        [MaxLength(6, ErrorMessage = "Code has to be maximum of 6 digits")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="Name has to be a maximum of 50 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
