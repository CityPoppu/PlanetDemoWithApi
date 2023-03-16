using Microsoft.AspNetCore.Mvc;
using PlanetDemoWithApi.Server.Repositories;
using PlanetDemoWithApi.Shared;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

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
        public IEnumerable<Planet> Get()
        {
            List<Planet> planets = new List<Planet>();
            planets = _planetRepository.GetPlanets();
            return planets;
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

        [HttpGet("GetDwarfPlanets")]
        public Planet GetDwarfPlanets()
        {
            List<Planet> planets = new List<Planet>();
            planets = _planetRepository.GetPlanets();
            return planets.First();
            //return planets;
        }

        [HttpGet("GetPlanetsExcludingDwarves")]
        public Planet GetPlanetsExcludingDwarves()
        {
            List<Planet> planets = new List<Planet>();
            planets = _planetRepository.GetPlanets();
            return planets.First();
            //return planets;
        }

    }
}