using System.Text.Json.Serialization;

namespace Lektion_SUT24_250414_API_intro.Models
{
    public class ActorMovie
    {
        public int ActorId { get; set; }
        public int MovieId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Actor? Actor { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Movie? Movie { get; set; }
    }
}
