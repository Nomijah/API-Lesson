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
    public class DirectorController : ControllerBase
    {

        private readonly MovieDbContext _context;

        public DirectorController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetDirectors")]
        public async Task<ActionResult<ICollection<Director>>> GetDirectors()
        {
            return Ok(await _context.Directors.Include(m => m.Movies).ToListAsync());
        }

        [HttpGet("{id}", Name = "GetDirectorById")]
        public async Task<ActionResult<Director>> GetDirectorById(int id)
        {
            var director = await _context.Directors.Include(d => d.Movies).FirstOrDefaultAsync(d => d.Id == id);
            if (director == null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }
            return Ok(director);
        }

        [HttpPost(Name = "CreateDirector")]
        public async Task<IActionResult> CreateDirector(Director newDirector)
        {
            if (newDirector == null)
            {
                return BadRequest(new { errorMessage = "Data missing." });
            }
            _context.Directors.Add(newDirector);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDirectorById), new { id = newDirector.Id }, newDirector);
        }

        [HttpPut("{id}", Name = "UpdateDirector")]
        public async Task<IActionResult> UpdateDirector(int id, Director updatedDirector)
        {

            var directorToUpdate = _context.Directors.Find(id);
            if (directorToUpdate == null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }

            directorToUpdate.FirstName = updatedDirector.FirstName;
            directorToUpdate.LastName = updatedDirector.LastName;
            directorToUpdate.BirthYear = updatedDirector.BirthYear;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteDirector")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var directorToDelete = _context.Directors.Find(id);
            if (directorToDelete is null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }
            _context.Directors.Remove(directorToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
