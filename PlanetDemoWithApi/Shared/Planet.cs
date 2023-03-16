namespace PlanetDemoWithApi.Shared
{
    public class Planet
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal DistanceFromSun { get; set; }  
        public string ActualMass { get; set; }
        public string EarthMass { get; set; }
        public int Diameter { get; set; }
        public int SurfaceTemperature { get; set; }
        public string Classification { get; set; }
        public int LengthOfYear { get; set; }
        public int NumberOfMoons { get; set; }
        public string Namesake { get; set; }
    }
}
