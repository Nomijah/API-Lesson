using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lektion_SUT24_250414_API_intro.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public TimeSpan Length { get; set; }
        public string? Genre { get; set; }
        public int DirectorId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Director? Director { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Actor>? Actors { get; set; }
    }
}
