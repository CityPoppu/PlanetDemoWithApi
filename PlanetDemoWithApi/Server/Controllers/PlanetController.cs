using Microsoft.AspNetCore.Mvc;
using PlanetDemoWithApi.Server.Repositories;
using PlanetDemoWithApi.Shared;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Numerics;

namespace PlanetDemoWithApi.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanetController : ControllerBase
    {
        private readonly ILogger<PlanetController> _logger;
        private IPlanetRepository _planetRepository;

        public PlanetController(ILogger<PlanetController> logger, IPlanetRepository planetRepository)
        {
            _logger = logger;
            _planetRepository = planetRepository;
        }

        
        [HttpGet]
        [ProducesResponseType(typeof(Planet), 200)]
        public IActionResult Get()
        {
            List<Planet> planets = new List<Planet>();
            planets = _planetRepository.GetPlanets();
            return Ok(planets);
        }

        [HttpGet("GetPlanetByName/{name}")]
        [ProducesResponseType(typeof(Planet), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPlanetByName(string name)
        {
            var planet = _planetRepository.GetPlanetByName(name);

            if (planet == null) 
            {
                return NotFound("No planets were found in our Solar System by that name");
            }

            return Ok(planet);
        }

        [HttpGet("GetPlanetsFromProperty")]
        public IActionResult GetPlanetsFromProperty(string? classification, int? numberOfMoons)
        {
            var planets = _planetRepository.GetPlanetsFromProperty(classification, numberOfMoons);

            if (planets.Count == 0)
            {
                return NotFound("No planets were found in our Solar System searching using those properties");
            }

            return Ok(planets);
        }


    }
}