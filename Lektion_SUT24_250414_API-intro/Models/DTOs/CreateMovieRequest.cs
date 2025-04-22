using System.ComponentModel.DataAnnotations;

namespace Lektion_SUT24_250414_API_intro.Models.DTOs
{
    public class CreateMovieRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public TimeSpan Length { get; set; } = TimeSpan.Zero;
        public string? Genre { get; set; }
        public int DirectorId { get; set; }
        public int[]? ActorIds { get; set; }
    }
}
