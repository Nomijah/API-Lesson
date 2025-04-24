using Lektion_SUT24_250414_API_intro.Data;
using Lektion_SUT24_250414_API_intro.Models;
using Microsoft.EntityFrameworkCore;

namespace Lektion_SUT24_250414_API_intro.Repositories
{
    public class DirectorRepository : GenericRepository<Director>, IDirectorRepository
    {
        private readonly MovieDbContext _context;

        public DirectorRepository(MovieDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Director?> GetDirectorWithMoviesAsync(int id)
        {
            return await _context.Directors.Include(d => d.Movies).FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
