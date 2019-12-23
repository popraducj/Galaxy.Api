using System;
using Galaxy.Api.Core.Enums.Planets;
using Galaxy.Planets;

namespace Galaxy.Api.Infrastructure.Helpers
{
    public static class PlanetExtensions
    {
        public static PlanetModel ToPlanetModel(this Core.Models.Planets.Planet planet)
        {
            if (planet == null) return new PlanetModel();
            return new PlanetModel()
            {
                Id = planet.Id.ToString(),
                Description = planet.Description ?? string.Empty,
                Name = planet.Name ?? string.Empty,
                Status = (int) planet.Status,
                Units = planet.Units,
                ImagePath = planet.ImagePath ?? string.Empty
            };
        }

        public static Core.Models.Planets.Planet ToPlanet(this PlanetModel planetModel)
        {
            return new Core.Models.Planets.Planet
            {
                Id = Guid.Parse(planetModel.Id),
                Description = planetModel.Description,
                Name = planetModel.Name,
                Status = (PlanetStatus) planetModel.Status,
                Units = planetModel.Units,
                ImagePath = planetModel.ImagePath
            };
        }
    }
}