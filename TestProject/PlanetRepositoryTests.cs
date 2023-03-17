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

        [TestMethod]
        public void TestJupiterIsReturnedFromName()
        {
            var config = InitConfiguration();
            // Arrange
            var testRepository = new PlanetRepository(config);

            //Act
            var result = testRepository.GetPlanetByName("Jupiter");

            // Assert
            Assert.IsTrue(result.Name == "Jupiter");
        }

        [TestMethod]
        public void TestJupiterIsReturnedFromName_Lowecase()
        {
            var config = InitConfiguration();
            // Arrange
            var testRepository = new PlanetRepository(config);

            //Act
            var result = testRepository.GetPlanetByName("jupiter");

            // Assert
            Assert.IsTrue(result.Name == "Jupiter");
        }

        [TestMethod]
        public void TestOnlyDwarfPlanetsAreReturned()
        {
            var config = InitConfiguration();
            // Arrange
            var testRepository = new PlanetRepository(config);

            //Act
            var result = testRepository.GetPlanetsFromProperty("Dwarf Planet", null);

            // Assert
            Assert.IsTrue(result.Count == 5);
        }


        [TestMethod]
        public void TestOnlyDwarfPlanetsAreReturned_lowercase()
        {
            var config = InitConfiguration();
            // Arrange
            var testRepository = new PlanetRepository(config);

            //Act
            var result = testRepository.GetPlanetsFromProperty("dwarf planet", null);

            // Assert
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void TestTwoPlanetsAreReturnedFromMoonCount()
        {
            var config = InitConfiguration();
            // Arrange
            var testRepository = new PlanetRepository(config);

            //Act
            var result = testRepository.GetPlanetsFromProperty(null, 2);

            // Assert
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Select(p => p.Name).Contains("Mars"));
            Assert.IsTrue(result.Select(p => p.Name).Contains("Haumea"));
        }

        [TestMethod]
        public void TestSaturnOnlyIsReturnedFromMoonCount()
        {
            var config = InitConfiguration();
            // Arrange
            var testRepository = new PlanetRepository(config);

            //Act
            var result = testRepository.GetPlanetsFromProperty(null, 83);

            // Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault().Name == "Saturn");
        }

        [TestMethod]
        public void TestOnlyMarsIsReturnedFromMoonCountAndClassification()
        {
            var config = InitConfiguration();
            // Arrange
            var testRepository = new PlanetRepository(config);

            //Act
            var result = testRepository.GetPlanetsFromProperty("Terrestial Planet", 2);

            // Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault().Name == "Mars");
        }

    }
}