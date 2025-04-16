namespace Lektion_SUT24_250414_API_intro.Models
{
    public class Farm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<string>? Animals { get; set; }
    }
}
