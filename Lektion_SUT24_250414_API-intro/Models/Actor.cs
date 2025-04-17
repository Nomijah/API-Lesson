using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lektion_SUT24_250414_API_intro.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public int BirthYear { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Movie>? Movies { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<ActorMovie>? ActorMovies { get; set; }
    }
}
