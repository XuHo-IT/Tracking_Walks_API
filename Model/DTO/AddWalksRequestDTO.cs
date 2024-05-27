using System.ComponentModel.DataAnnotations;

namespace NZWalk.API.Model.DTO
{
    public class AddWalksRequestDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength (1000)]
        public string Description { get; set; }
        [Required]
        [Range(0,100)]
        public double LengthInkm { get; set; }

        public string? WalkImageUrl { get; set; }
        [Required]

        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
