using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lektion_SUT24_250414_API_intro.Models.DTOs
{
    public class GetMovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public TimeSpan Length { get; set; }
        public string? Genre { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Actor>? Actors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Director? Director { get; set; }

        public GetMovieResponse(int id, string title, TimeSpan length, string? genre, ICollection<Actor>? actors, Director? director)
        {
            Id = id;
            Title = title;
            Length = length;
            Genre = genre;
            Actors = actors;
            Director = director;
        }
    }
}
