using System.ComponentModel;

namespace PlanetDemoWithApi.Shared
{
    public class Planet
    {
        [Description("Name of the Planet")]
        public string Name { get; set; }
        [Description("URL that will be used to display a photo of the Planet")]
        public string ImageUrl { get; set; }
        [Description("Distance from the Sun in Astronomical Units")]
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
