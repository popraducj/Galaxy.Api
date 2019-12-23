using System;
using System.Collections.Generic;
using Galaxy.Api.Core.Enums.Planets;

namespace Galaxy.Api.Core.Models.Planets
{
    public class Exploration
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public Guid PlanetId { get; set; }
        public Guid TeamId { get; set; }
        public ExplorationStatus Status { get; set; }
        public DateTime PhaseFinishTime { get; set; }
        
        public List<ExplorationResultStatus> RobotsReports { get; set; } = new List<ExplorationResultStatus>();
    }
}