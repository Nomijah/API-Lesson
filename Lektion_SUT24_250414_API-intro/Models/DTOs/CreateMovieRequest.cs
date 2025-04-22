namespace Lektion_SUT24_250414_API_intro.Models.DTOs
{
    public record CreateMovieRequest
    {
        public string Title { get; init; } = string.Empty;
        public TimeSpan Length { get; init; } = TimeSpan.Zero;
        public string? Genre { get; init; }
        public int DirectorId { get; init; }
        public int[]? ActorIds { get; init; }
    }
}