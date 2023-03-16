using MySql.Data.MySqlClient;
using PlanetDemoWithApi.Shared;

namespace PlanetDemoWithApi.Server.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {

        private MySqlConnection EstablishSqlConnection()
        {
            string connectionString = "Server=aws-eu-west-2.connect.psdb.cloud;Database=planetdemodb;user=76gl80thw2v4qkftka1n;password=pscale_pw_35FocMjFUZhVCDwvYHWjVqBJZW4MR3s5SJxh0YYds5y;SslMode=VerifyFull;";
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
