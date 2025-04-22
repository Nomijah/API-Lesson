using System.Text.Json.Serialization;

namespace Lektion_SUT24_250414_API_intro.Models.DTOs
{
    public class GetMovieResponse
    {
        public string Title { get; set; } = string.Empty;
        public TimeSpan Length { get; set; }
        public string? Genre { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Actor>? Actors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Director? Director { get; set; }
    }
}
