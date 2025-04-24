using Lektion_SUT24_250414_API_intro.Models;

namespace Lektion_SUT24_250414_API_intro.Repositories
{
    public interface IDirectorRepository : IGenericRepository<Director>
    {
        public Task<Director?> GetDirectorWithMoviesAsync(int id);
    }
}
