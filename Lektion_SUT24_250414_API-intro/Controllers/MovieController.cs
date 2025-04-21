using Lektion_SUT24_250414_API_intro.Data;
using Lektion_SUT24_250414_API_intro.Models;
using Lektion_SUT24_250414_API_intro.Models.DTOs;
using Lektion_SUT24_250414_API_intro.Repositories;
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
        private readonly IMovieRepository _movieRepo;

        public MovieController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        [HttpGet(Name = "GetMovies")]
        public async Task<ActionResult<ICollection<Movie>>> GetMovies()
        {
            return Ok(await _movieRepo.Get());
            //return Ok(await _context.Movies
            //    .Select(m => new GetMovieResponse( m.Id, m.Title, m.Length, m.Genre, m.Actors , m.Director))
            //    .ToListAsync());
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _movieRepo.GetById(id);
            //var movie = await _context.Movies
            //    .Select(m => new GetMovieResponse(m.Id, m.Title, m.Length, m.Genre, m.Actors, m.Director))
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = "Filmen hittades inte."});
            }
            return Ok(movie);
        }

        [HttpPost(Name = "CreateMovie")]
        public async Task<IActionResult> CreateMovie(CreateMovieRequest newMovie)
        {
            if (newMovie == null)
            {
                return BadRequest(new { errorMessage = "Data missing." });
            }

            //var actorList = new List<Actor>();
            //if(newMovie.ActorIds.Length > 0)
            //{
            //    foreach (var id in newMovie.ActorIds)
            //    {
            //        var actor = _context.Actors.Find(id);
            //        if (actor != null)
            //        {
            //            actorList.Add(actor);
            //        }
            //    }
            //}


            var movieToAdd = new Movie()
            {
                Title = newMovie.Title,
                Genre = newMovie.Genre,
                Length = newMovie.Length,
                DirectorId = newMovie.DirectorId
            };


            await _movieRepo.Create(movieToAdd);

            return CreatedAtAction(nameof(GetMovieById), new { id = movieToAdd.Id}, movieToAdd);
        }

        [HttpPut("{id}", Name = "UpdateMovie")]
        public async Task<IActionResult> UpdateMovie(int id, CreateMovieRequest updatedMovie)
        {
            var movieToUpdate = await _movieRepo.GetById(id);

            if (movieToUpdate == null)
            {
                return NotFound(new { errorMessage = "Filmen hittades inte." });
            }

            //var validActorIds = await _context.Actors
            //    .Where(a => updatedMovie.ActorIds.Contains(a.Id))
            //    .Select(a => a.Id)
            //    .ToListAsync();

            //if (validActorIds.Count != updatedMovie.ActorIds.Length)
            //{
            //    return BadRequest(new { errorMessage = "One or more actor IDs are invalid." });
            //}

            //// Attach actors to the movie
            //var actorList = validActorIds.Select(id => new Actor { Id = id }).ToList();
            //foreach (var actor in actorList)
            //{
            //    _context.Actors.Attach(actor);
            //}

            movieToUpdate.Length = updatedMovie.Length;
            movieToUpdate.Title = updatedMovie.Title;
            movieToUpdate.DirectorId = updatedMovie.DirectorId;
            movieToUpdate.Genre = updatedMovie.Genre;

            await _movieRepo.Update(movieToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movieToDelete = await _movieRepo.GetById(id);
            if (movieToDelete is null)
            {
                return NotFound(new { errorMessage = "Filmen hittades inte." });
            }
            await _movieRepo.Delete(movieToDelete);
            return NoContent();
        }
    }
}
