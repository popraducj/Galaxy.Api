using System;
using Galaxy.Api.Core.Enums.Planets;

namespace Galaxy.Api.Core.Models.Planets
{
    public class Planet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int Units { get; set; }
        public PlanetStatus Status { get; set; }
    }
}