using Lektion_SUT24_250414_API_intro.Data;
using Lektion_SUT24_250414_API_intro.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Lektion_SUT24_250414_API_intro.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _context;

        public MovieRepository(MovieDbContext context)
        {
            _context = context;
        }

        public async Task Create(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Movie>> Get()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task Update(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }
    }
}
