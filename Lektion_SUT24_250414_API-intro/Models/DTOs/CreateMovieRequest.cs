using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lektion_SUT24_250414_API_intro.Models.DTOs
{
    public record CreateMovieRequest
    {
        public string Title { get; set; } = string.Empty;
        public TimeSpan Length { get; set; }
        public string? Genre { get; set; }
        public int DirectorId { get; set; }
        public int[]? ActorIds { get; set; }
    }
}
