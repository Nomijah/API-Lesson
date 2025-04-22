using Lektion_SUT24_250414_API_intro.Data;
using Lektion_SUT24_250414_API_intro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lektion_SUT24_250414_API_intro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public MovieController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetMovies")]
        public async Task<ActionResult<ICollection<Movie>>> GetMovies()
        {
            //return Ok(await _context.Movies.Include(m => m.Director).Include(m => m.Actors).ToListAsync());
            return Ok(await _context.Movies.Select(m => new { m.Title, m.Length, m.Genre, m.Director, m.Actors }).ToListAsync());
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = "Filmen hittades inte."});
            }
            return Ok(movie);
        }

        [HttpPost(Name = "CreateMovie")]
        public async Task<IActionResult> CreateMovie(Movie newMovie)
        {
            if (newMovie == null)
            {
                return BadRequest(new { errorMessage = "Data missing." });
            }
            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovieById), new { id = newMovie.Id}, newMovie);
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        public async Task<IActionResult> UpdateMovie(int id, Movie updatedMovie)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var movieToUpdate = _context.Movies.Find(id);
            Console.WriteLine(sw.ElapsedMilliseconds);
            if (movieToUpdate == null)
            {
                return NotFound(new { errorMessage = "Filmen hittades inte." });
            }

            movieToUpdate.Length = updatedMovie.Length;
            movieToUpdate.Title = updatedMovie.Title;
            movieToUpdate.DirectorId = updatedMovie.DirectorId;
            movieToUpdate.Actors = updatedMovie.Actors;
            movieToUpdate.Genre = updatedMovie.Genre;

            await _context.SaveChangesAsync();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movieToDelete = _context.Movies.Find(id);
            if (movieToDelete is null)
            {
                return NotFound(new { errorMessage = "Filmen hittades inte." });
            }
            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
