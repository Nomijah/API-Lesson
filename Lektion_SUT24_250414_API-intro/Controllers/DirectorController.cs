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
    public class DirectorController : ControllerBase
    {

        private readonly IDirectorRepository _directorRepo;

        public DirectorController(IDirectorRepository directorRepo)
        {
            _directorRepo = directorRepo;
        }

        [HttpGet(Name = "GetDirectors")]
        public async Task<ActionResult<ICollection<Director>>> GetDirectors()
        {
            return Ok(await _directorRepo.GetAsync());
        }

        [HttpGet("{id}", Name = "GetDirectorById")]
        public async Task<ActionResult<Director>> GetDirectorById(int id)
        {
            var director = await _directorRepo.GetByIdAsync(id);
            if (director == null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }
            return Ok(director);
        }

        [HttpPost(Name = "CreateDirector")]
        public async Task<IActionResult> CreateDirector(CreateDirectorRequest newDirector)
        {
            if (newDirector == null)
            {
                return BadRequest(new { errorMessage = "Data missing." });
            }
            var directorToCreate = new Director(newDirector.FirstName, newDirector.LastName, newDirector.BirthYear);
            await _directorRepo.CreateAsync(directorToCreate);

            return CreatedAtAction(nameof(GetDirectorById), new { id = directorToCreate.Id }, directorToCreate);
        }

        [HttpPut("{id}", Name = "UpdateDirector")]
        public async Task<IActionResult> UpdateDirector(int id, CreateDirectorRequest updatedDirector)
        {

            var directorToUpdate = await _directorRepo.GetByIdAsync(id);
            if (directorToUpdate == null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }

            directorToUpdate.FirstName = updatedDirector.FirstName;
            directorToUpdate.LastName = updatedDirector.LastName;
            directorToUpdate.BirthYear = updatedDirector.BirthYear;

            await _directorRepo.UpdateAsync(directorToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteDirector")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var directorToDelete = await _directorRepo.GetByIdAsync(id);
            if (directorToDelete is null)
            {
                return NotFound(new { errorMessage = "Regissören hittades inte." });
            }
            await _directorRepo.DeleteAsync(directorToDelete);
            return NoContent();
        }
    }
}
