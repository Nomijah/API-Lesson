using Lektion_SUT24_250414_API_intro.Data;
using Lektion_SUT24_250414_API_intro.Models;
using Lektion_SUT24_250414_API_intro.Models.DTOs;
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
        public async Task<ActionResult<ICollection<GetMovieResponse>>> GetMovies()
        {
            //return Ok(await _context.Movies.Include(m => m.Director).Include(m => m.Actors).ToListAsync());
            return Ok(await _context.Movies
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Length = m.Length,
                    Genre = m.Genre,
                    Director = m.Director,
                    Actors = m.Actors
                }).ToListAsync());
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<ActionResult<GetMovieResponse>> GetMovieById(int id)
        {
            var movie = await _context.Movies
                .Select(m => new MovieDto
                { 
                    Id = m.Id, 
                    Title = m.Title, 
                    Length = m.Length, 
                    Genre = m.Genre, 
                    Director =  m.Director, 
                    Actors = m.Actors 
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound(new { errorMessage = "Filmen hittades inte." });
            }

            var movieResponse = new GetMovieResponse()
            {
                Title = movie.Title,
                Genre = movie.Genre,
                Length = movie.Length,
                Director = movie.Director ?? new Director(),
                Actors = movie.Actors ?? new List<Actor>()
            };

            return Ok(movieResponse);
        }

        [HttpPost(Name = "CreateMovie")]
        public async Task<IActionResult> CreateMovie(CreateMovieRequest newMovie)
        {
            if (newMovie == null)
            {
                return BadRequest(new { errorMessage = "Data missing." });
            }

            var actorList = new List<Actor>();

            if (newMovie.ActorIds != null)
            {
                var validActorIds = await _context.Actors
                    .Where(a => newMovie.ActorIds.Contains(a.Id))
                    .Select(a => a.Id)
                    .ToListAsync();

                if (validActorIds.Count != newMovie.ActorIds.Length)
                {
                    return BadRequest(new { errorMessage = "One or more actor IDs are invalid." });
                }

                actorList = validActorIds.Select(id => new Actor { Id = id }).ToList();

                foreach (var actor in actorList)
                {
                    _context.Actors.Attach(actor);
                }
            }

            var movieToAdd = new Movie()
            {
                Title = newMovie.Title,
                Genre = newMovie.Genre,
                Length = newMovie.Length,
                DirectorId = newMovie.DirectorId,
                Actors = actorList
            };

            _context.Movies.Add(movieToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovieById), new { id = movieToAdd.Id }, movieToAdd);
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        public async Task<IActionResult> UpdateMovie(int id, CreateMovieRequest updatedMovie)
        {

            var movieToUpdate = _context.Movies.Find(id);

            if (movieToUpdate == null)
            {
                return NotFound(new { errorMessage = "Filmen hittades inte." });
            }

            var actorList = new List<Actor>();

            if (updatedMovie.ActorIds != null)
            {
                var validActorIds = await _context.Actors
                    .Where(a => updatedMovie.ActorIds.Contains(a.Id))
                    .Select(a => a.Id)
                    .ToListAsync();

                if (validActorIds.Count != updatedMovie.ActorIds.Length)
                {
                    return BadRequest(new { errorMessage = "One or more actor IDs are invalid." });
                }
                actorList = validActorIds.Select(id => new Actor { Id = id }).ToList();

                foreach (var actor in actorList)
                {
                    _context.Actors.Attach(actor);
                }
            }

            movieToUpdate.Length = updatedMovie.Length;
            movieToUpdate.Title = updatedMovie.Title;
            movieToUpdate.DirectorId = updatedMovie.DirectorId;
            movieToUpdate.Actors = actorList;
            movieToUpdate.Genre = updatedMovie.Genre;

            await _context.SaveChangesAsync();

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
