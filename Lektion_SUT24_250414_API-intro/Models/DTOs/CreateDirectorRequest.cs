using System.ComponentModel.DataAnnotations;

namespace Lektion_SUT24_250414_API_intro.Models.DTOs
{
    public class CreateDirectorRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public int BirthYear { get; set; }
    }
}
