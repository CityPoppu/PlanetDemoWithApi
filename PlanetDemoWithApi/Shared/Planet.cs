using System.ComponentModel;

namespace PlanetDemoWithApi.Shared
{
    public class Planet
    {
        [Description("Name of the Planet")]
        public string Name { get; set; }
        [Description("URL that will be used to display a photo of the planet")]
        public string ImageUrl { get; set; }
        [Description("Distance from the Sun in Astronomical Units")]
        public decimal DistanceFromSun { get; set; }
        [Description("Mass of planet in billions of kilograms")]
        public string ActualMass { get; set; }
        [Description("Mass of planet in relation to Earth (M⊕)")]
        public string EarthMass { get; set; }
        [Description("Equatorial diameter of planet in kilometers")]
        public int Diameter { get; set; }
        [Description("Average surface temperature of the planet in celsius")]
        public int SurfaceTemperature { get; set; }
        [Description("Classification of the planet type, e.g. Dwarf Planets, Gas Giant")]
        public string Classification { get; set; }
        [Description("Length of a year on the planet in Earth Days")]
        public int LengthOfYear { get; set; }
        [Description("Number of moons the planet has")]
        public int NumberOfMoons { get; set; }
        [Description("What the planet is named after")]
        public string Namesake { get; set; }
    }
}
