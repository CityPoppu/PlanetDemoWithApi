

using PlanetDemoWithApi.Shared;

namespace PlanetDemoWithApi.Server.Repositories
{
    public interface IPlanetRepository
    {
        List<Planet> GetPlanets();
    }
}
