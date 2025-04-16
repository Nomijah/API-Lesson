using Lektion_SUT24_250414_API_intro.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lektion_SUT24_250414_API_intro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private static List<Farm> _farms = new List<Farm>
        {
            new Farm {Id = 1, Name = "Djurgården", Animals = ["Häst", "Hund", "Ko"]},
            new Farm {Id = 2, Name = "Kloträsk", Animals = ["Katt", "Hund", "Ko"]},
            new Farm {Id = 3, Name = "Hemgården", Animals = ["Häst", "Flodhäst", "Zebra"]}
        };

        [HttpGet(Name = "GetAllFarms")]
        public IEnumerable<Farm> GetAllFarms()
        {
            return _farms;
        }

        [HttpGet("{id}",  Name = "GetFarmById" )]
        public Farm GetFarmById(int id)
        {
            var farm = _farms.FirstOrDefault(f => f.Id == id);
            if (farm == null)
            {
                return new Farm();
            }
            return farm;
        }
    }
}
