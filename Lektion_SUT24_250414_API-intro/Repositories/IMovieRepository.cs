using Lektion_SUT24_250414_API_intro.Models;

namespace Lektion_SUT24_250414_API_intro.Repositories
{
    public interface IMovieRepository
    {
        public Task<ICollection<Movie>> Get();
        public Task<Movie?> GetById(int id);
        public Task Create(Movie movie);
        public Task Update(Movie movie);
        public Task Delete(Movie movie);
    }
}
