using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lektion_SUT24_250414_API_intro.Models
{
    public class Director
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public int BirthYear { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Movie>? Movies { get; set; }

        public Director() { }
        public Director(string firstName, string? lastName, int birthYear)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthYear = birthYear;
        }

        public Director(int id, string firstName, string? lastName, int birthYear) : this(firstName, lastName, birthYear)
        {
            Id = id;
        }
    }
}
