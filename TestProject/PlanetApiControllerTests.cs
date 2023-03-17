using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PlanetDemoWithApi.Server.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using PlanetDemoWithApi.Shared;
using Moq;
using PlanetDemoWithApi.Server.Controllers;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TestProject
{
    [TestClass]
    public class PlanetApiControllerTests
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
        public void TestOKReturnedFromApiForAllPlanets()
        {
            // Arrange
            var mockRepo = new Mock<IPlanetRepository>();
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, mockRepo.Object);

            //Act
            var result = controller.Get();

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 200);
        }

        [TestMethod]
        public void TestOKReturnedFromApiForNameSearchForPlanets()
        {
            // Arrange
            var config = InitConfiguration();
            var testRepository = new PlanetRepository(config);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, testRepository);

            //Act
            var result = controller.GetPlanetByName("Earth");

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 200);
        }

        [TestMethod]
        public void TestNotFoundReturnedFromApiForNameSearchForPlanets()
        {
            // Arrange
            var config = InitConfiguration();
            var testRepository = new PlanetRepository(config);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, testRepository);

            //Act
            var result = controller.GetPlanetByName("Test - this does not exist");

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 404);
        }

        [TestMethod]
        public void TestOKReturnedFromApiForCorrectClassification()
        {
            // Arrange
            var config = InitConfiguration();
            var testRepository = new PlanetRepository(config);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, testRepository);

            //Act
            var result = controller.GetPlanetsFromProperty("Gas Giant", null);

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 200);
        }

        [TestMethod]
        public void TestNotFoundReturnedFromApiForIncorrectClassification()
        {
            // Arrange
            var config = InitConfiguration();
            var testRepository = new PlanetRepository(config);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, testRepository);

            //Act
            var result = controller.GetPlanetsFromProperty("Test - this does not exist", null);

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 404);
        }

        [TestMethod]
        public void TestOKReturnedFromApiForCorrectNumberOfMoons()
        {
            // Arrange
            var config = InitConfiguration();
            var testRepository = new PlanetRepository(config);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, testRepository);

            //Act
            var result = controller.GetPlanetsFromProperty(null, 2);

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 200);
        }

        [TestMethod]
        public void TestNotFoundReturnedFromApiForIncorrectNumberOfMoons()
        {
            // Arrange
            var config = InitConfiguration();
            var testRepository = new PlanetRepository(config);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, testRepository);

            //Act
            var result = controller.GetPlanetsFromProperty(null, 9999);

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 404);
        }

        [TestMethod]
        public void TestOkReturnedFromApiForCorrectClassificationAndNumberOfMoons()
        {
            // Arrange
            var config = InitConfiguration();
            var testRepository = new PlanetRepository(config);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, testRepository);

            //Act
            var result = controller.GetPlanetsFromProperty("Terrestial Planet", 2);

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 200);
        }

        [TestMethod]
        public void TestNotFoundReturnedFromApiForIncorrectClassificationAndNumberOfMoons()
        {
            // Arrange
            var config = InitConfiguration();
            var testRepository = new PlanetRepository(config);

            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<PlanetController>>();
            var controller = new PlanetController(mockLogger.Object, testRepository);

            //Act
            var result = controller.GetPlanetsFromProperty("Test Failure", 9999);

            // Assert;
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 404);
        }
    }
}