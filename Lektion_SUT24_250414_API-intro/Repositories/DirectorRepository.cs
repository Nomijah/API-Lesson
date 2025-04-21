using Lektion_SUT24_250414_API_intro.Data;
using Lektion_SUT24_250414_API_intro.Models;

namespace Lektion_SUT24_250414_API_intro.Repositories
{
    public class DirectorRepository : GenericRepository<Director>, IDirectorRepository
    {
        private readonly MovieDbContext _context;

        public DirectorRepository(MovieDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
