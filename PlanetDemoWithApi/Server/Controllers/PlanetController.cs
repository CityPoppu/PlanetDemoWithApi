using Microsoft.AspNetCore.Mvc;
using PlanetDemoWithApi.Server.Repositories;
using PlanetDemoWithApi.Shared;

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
        public IEnumerable<Planet> Get()
        {
            List<Planet> planets = new List<Planet>();
            planets = _planetRepository.GetPlanets();
            return planets;
        }
    }
}