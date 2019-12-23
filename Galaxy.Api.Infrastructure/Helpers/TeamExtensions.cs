using System;
using System.Collections.Generic;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Teams;

namespace Galaxy.Api.Infrastructure.Helpers
{
    public static class TeamExtensions
    {
        public static TeamModel ToTeamModel(this Core.Models.Teams.Team team)
        {
            var  response = new TeamModel
            {
                Id = team.Id.ToString(),
                Name = team.Name ?? string.Empty,
                Status = (int) team.Status,
                CaptainId = team.CaptainId.ToString(),
                ShuttleId = team.ShuttleId.ToString()
            };
            team.Robots.ForEach(robot => response.RobotsIds.Add(robot.ToString()));
            return response;
        }
        public static Core.Models.Teams.Team ToTeam(this TeamModel team)
        {
            if(!Guid.TryParse(team.Id, out _)) return new Core.Models.Teams.Team();
            
            var response = new Core.Models.Teams.Team
            {
                Id = Guid.Parse(team.Id),
                Name = team.Name,
                Status = (TeamStatus) team.Status,
                CaptainId = Guid.Parse(team.CaptainId),
                ShuttleId = Guid.Parse(team.ShuttleId),
                Robots = new List<Guid>()
            };
            foreach (var robotId in team.RobotsIds)
            {
                response.Robots.Add(Guid.Parse(robotId));
            }

            return response;
        }
    }
}