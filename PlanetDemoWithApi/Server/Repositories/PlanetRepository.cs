using MySql.Data.MySqlClient;
using PlanetDemoWithApi.Shared;

namespace PlanetDemoWithApi.Server.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {

        public List<Planet> GetPlanets()
        {
            List<Planet> queryResults = new List<Planet>();

            string connStr = "Server=aws-eu-west-2.connect.psdb.cloud;Database=planetdemodb;user=76gl80thw2v4qkftka1n;password=pscale_pw_35FocMjFUZhVCDwvYHWjVqBJZW4MR3s5SJxh0YYds5y;SslMode=VerifyFull;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "SELECT name, imageURL, distanceFromSun, actualMass, earthMass, diameter, surfaceTemperatureAverage, classification, lengthOfYear, numberOfMoons, namesake FROM Planets";
                using var cmd = new MySqlCommand(sql, conn);

                using MySqlDataReader rdr = cmd.ExecuteReader();


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

                conn.Close();
            }
            catch (Exception ex)
            {
                //TODO: Replace with logging
                Console.WriteLine(ex.ToString());
            }

            return queryResults.OrderByDescending(p => p.DistanceFromSun).ToList();
        }
    }
}
