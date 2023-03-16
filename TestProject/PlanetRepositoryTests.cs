using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PlanetDemoWithApi.Server.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


namespace TestProject
{
    [TestClass]
    public class PlanetRepositoryTests
    {

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
               .AddEnvironmentVariables()
               .Build();
            return config;
        }

        [TestMethod]
        public void TestAllPlanetsAreReturned()
        {
            var config = InitConfiguration();
            // Arrange
            var testRepository = new PlanetRepository(config);

            //Act
            var result = testRepository.GetPlanets();

            // Assert
            Assert.IsTrue(result.Count == 13);
        }
    }
}