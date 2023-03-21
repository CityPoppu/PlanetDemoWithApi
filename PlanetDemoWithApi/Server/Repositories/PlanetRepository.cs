using MySql.Data.MySqlClient;
using PlanetDemoWithApi.Shared;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Org.BouncyCastle.Asn1.Cms;
using System.Reflection.PortableExecutable;
using System.Data;

namespace PlanetDemoWithApi.Server.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly IConfiguration _configuration;
        private string _sqlForAllPlanets = "SELECT name, imageURL, distanceFromSun, actualMass, earthMass, diameter, surfaceTemperatureAverage, classification, " +
                "lengthOfYear, numberOfMoons, namesake FROM Planets";


        public PlanetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private Planet GetPlanetRow(MySqlDataReader rdr)
        {

            var planet = new Planet
            {
                Name = (string)rdr.GetValue("name"),
                ImageUrl = (string)rdr.GetValue("imageURL"),
                DistanceFromSun = Convert.ToDouble((decimal)rdr.GetValue("distanceFromSun")),
                ActualMass = (string)rdr.GetValue("actualMass"),
                EarthMass = Convert.ToDouble((decimal)rdr.GetValue("earthMass")),
                Diameter = (int)rdr.GetValue("diameter"),
                SurfaceTemperature = (int)rdr.GetValue("surfaceTemperatureAverage"),
                Classification = (string)rdr.GetValue("classification"),
                LengthOfYear = (int)rdr.GetValue("lengthOfYear"),
                NumberOfMoons = (int)rdr.GetValue("numberOfMoons"),
                Namesake = (string)rdr.GetValue("namesake"),
            };

            return planet;
        }

        private MySqlConnection EstablishSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("ReadOnly");
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            try
            {
                Console.WriteLine("Attempting to connect to " + connectionString);
                sqlConnection.Open();
                Console.WriteLine("Succesfully connected to data source " + connectionString);
            }

            catch(Exception ex)
            {
                Console.WriteLine("Exception encounter trying to connect to data source " + connectionString);
                Console.WriteLine(ex.ToString());
            }

            return sqlConnection;
        }


        public List<Planet> GetPlanets()
        {
            List<Planet> queryResults = new List<Planet>();

            var sqlConnection = EstablishSqlConnection();

            using var sqlCommand = new MySqlCommand(_sqlForAllPlanets, sqlConnection);
            
            using MySqlDataReader rdr = sqlCommand.ExecuteReader();

            while (rdr.Read())
            {
                var planet = GetPlanetRow(rdr);
                queryResults.Add(planet);
            };
            
            sqlConnection.Close();

            return queryResults;
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
                    planet = GetPlanetRow(rdr);
                }
            };

            sqlConnection.Close();

            return planet;
        }

        public List<Planet> GetPlanetsFromProperty(string? classification, int? numberOfMoons)
        {

            var queryResults = new List<Planet>();

            if (classification == null && numberOfMoons == null)
            {
                return queryResults;
            }

            var sql = _sqlForAllPlanets;

            if (classification != null)
            {
                sql = sql + $" WHERE UPPER(Classification) = UPPER('{classification}')";

                if (numberOfMoons != null)
                {
                    sql = sql + $" AND numberOfMoons = {numberOfMoons}";
                }
            }

            else
            {
                sql = sql + $" WHERE numberOfMoons = {numberOfMoons}";
            }

            var sqlConnection = EstablishSqlConnection();

            using var sqlCommand = new MySqlCommand(sql, sqlConnection);

            using MySqlDataReader rdr = sqlCommand.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetString(0) != null)
                {
                    var planet = GetPlanetRow(rdr);
                    queryResults.Add(planet);
                }
            };

            sqlConnection.Close();

            return queryResults;
        }
    }
}
