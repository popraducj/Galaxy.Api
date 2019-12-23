using System;
using System.Collections.Generic;
using Galaxy.Api.Core.Enums.Planets;
using Galaxy.Planets;

namespace Galaxy.Api.Infrastructure.Helpers
{
    public static class ExplorationExtensions
    {
        public static ExplorationModel ToExplorationModel(this Core.Models.Planets.Exploration exploration)
        {
            if (exploration == null) return new ExplorationModel();
            
            var result = new ExplorationModel
            {
                Id = exploration.Id.ToString(),
                Name = exploration.Name?? string.Empty,
                Status = (int) exploration.Status,
                PlanetId = exploration.PlanetId.ToString(),
                TeamId = exploration.TeamId.ToString()
            };
            exploration.RobotsReports.ForEach(status => result.RobotsReports.Add((int)status));

            return result;
        }

        public static Core.Models.Planets.Exploration ToExploration(this ExplorationModel model)
        {
            var result = new Core.Models.Planets.Exploration
            {
                Id = Guid.Parse(model.Id),
                Name = model.Name,
                Status = (ExplorationStatus) model.Status,
                PlanetId = Guid.Parse(model.PlanetId),
                TeamId = Guid.Parse(model.TeamId),
                RobotsReports = new List<ExplorationResultStatus>()
            };

            foreach (var report in model.RobotsReports)
            {
                result.RobotsReports.Add((ExplorationResultStatus) report);
            }

            return result;
        }
    }
}