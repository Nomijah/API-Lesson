using Lektion_SUT24_250414_API_intro.Data;
using Lektion_SUT24_250414_API_intro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lektion_SUT24_250414_API_intro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public ActorController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetActors")]
        public async Task<ActionResult<ICollection<Actor>>> GetActors()
        {
            return Ok(await _context.Actors.ToListAsync());
        }

        [HttpGet("{id}", Name = "GetActorById")]
        public async Task<ActionResult<Actor>> GetActorById(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(d => d.Id == id);
            if (actor == null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }
            return Ok(actor);
        }

        [HttpPost(Name = "CreateActor")]
        public async Task<IActionResult> CreateActor(Actor newActor)
        {
            if (newActor == null)
            {
                return BadRequest(new { errorMessage = "Data missing." });
            }
            _context.Actors.Add(newActor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActorById), new { id = newActor.Id }, newActor);
        }

        [HttpPut("{id}", Name = "UpdateActor")]
        public async Task<IActionResult> UpdateActor(int id, Actor updatedActor)
        {

            var actorToUpdate = _context.Actors.Find(id);
            if (actorToUpdate == null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }

            actorToUpdate.FirstName = updatedActor.FirstName;
            actorToUpdate.LastName = updatedActor.LastName;
            actorToUpdate.BirthYear = updatedActor.BirthYear;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteActor")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actorToDelete = _context.Actors.Find(id);
            if (actorToDelete is null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }
            _context.Actors.Remove(actorToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
