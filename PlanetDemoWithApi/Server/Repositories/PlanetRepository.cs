using MySql.Data.MySqlClient;
using PlanetDemoWithApi.Shared;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace PlanetDemoWithApi.Server.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly IConfiguration _configuration;

        public PlanetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection EstablishSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("ReadOnly");
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
            }

            catch(Exception ex)
            {
                //TODO: Replace with logging
                Console.WriteLine(ex.ToString());
            }

            return sqlConnection;
        }


        public List<Planet> GetPlanets()
        {
            List<Planet> queryResults = new List<Planet>();

            var sqlConnection = EstablishSqlConnection();

            string sql = "SELECT name, imageURL, distanceFromSun, actualMass, earthMass, diameter, surfaceTemperatureAverage, classification, " +
                "lengthOfYear, numberOfMoons, namesake FROM Planets";

            using var sqlCommand = new MySqlCommand(sql, sqlConnection);
            
            using MySqlDataReader rdr = sqlCommand.ExecuteReader();
            
            while (rdr.Read())
            {
                var planet = new Planet { 
                    Name = rdr.GetString(0), 
                    ImageUrl = rdr.GetString(1),
                    DistanceFromSun = rdr.GetDecimal(2),
                    ActualMass = rdr.GetString(3),
                    EarthMass = rdr.GetString(4),
                    Diameter = rdr.GetInt32(5),
                    SurfaceTemperature = rdr.GetInt32(6),
                    Classification = rdr.GetString(7),
                    LengthOfYear = rdr.GetInt32(8),
                    NumberOfMoons = rdr.GetInt32(9),
                    Namesake = rdr.GetString(10)
                };
                queryResults.Add(planet);
            };
            
            sqlConnection.Close();

            return queryResults.OrderBy(p => p.DistanceFromSun).ToList();
        }

        public Planet GetPlanetByName(string name)
        {
            var sqlConnection = EstablishSqlConnection();

            string sql = $"SELECT name, imageURL, distanceFromSun, actualMass, earthMass, diameter, surfaceTemperatureAverage, classification, " +
                $"lengthOfYear, numberOfMoons, namesake FROM Planets where UPPER(name) = UPPER('{name}')";

            using var sqlCommand = new MySqlCommand(sql, sqlConnection);

            using MySqlDataReader rdr = sqlCommand.ExecuteReader();

            Planet? planet = null;

            while (rdr.Read())
            {
                if (rdr.GetString(0) != null)
                {
                    planet = new Planet
                    {
                        Name = rdr.GetString(0),
                        ImageUrl = rdr.GetString(1),
                        DistanceFromSun = rdr.GetDecimal(2),
                        ActualMass = rdr.GetString(3),
                        EarthMass = rdr.GetString(4),
                        Diameter = rdr.GetInt32(5),
                        SurfaceTemperature = rdr.GetInt32(6),
                        Classification = rdr.GetString(7),
                        LengthOfYear = rdr.GetInt32(8),
                        NumberOfMoons = rdr.GetInt32(9),
                        Namesake = rdr.GetString(10)
                    };
                }
            };

            sqlConnection.Close();

            return planet;
        }
    }
}
