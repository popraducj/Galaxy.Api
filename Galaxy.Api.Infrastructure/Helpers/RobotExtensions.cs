using System;
using Galaxy.Api.Core.Enums;
using Galaxy.Robots.Presentation;

namespace Galaxy.Api.Infrastructure.Helpers
{
    public static class RobotExtensions
    {
        public static RobotModel ToRobotModel(this Core.Models.Teams.Robot robot)
        {
            return new RobotModel
            {
                Id = robot.Id.ToString(),
                Name = robot.Name,
                Status = (int) robot.Status,
                Manufacturer = robot.Manufacturer,
                Model = robot.Model,
                Year = robot.Year,
                NextRevision = robot.NextRevision.ToString("s"),
                TrustWorthyPercentage = robot.TrustWorthyPercentage,
                FuelConsumptionPerDay = robot.FuelConsumptionPerDay,
                UnitsCoveredInADay = robot.UnitsCoveredInADay
            };
        }
        public static Core.Models.Teams.Robot ToRobot(this RobotModel robot)
        {
            return new Core.Models.Teams.Robot
            {
                Id = Guid.Parse(robot.Id),
                Name = robot.Name,
                Status = (RobotStatus) robot.Status,
                Manufacturer = robot.Manufacturer,
                Model = robot.Model,
                Year = robot.Year,
                NextRevision = DateTime.Parse(robot.NextRevision),
                TrustWorthyPercentage = robot.TrustWorthyPercentage,
                FuelConsumptionPerDay = robot.FuelConsumptionPerDay,
                UnitsCoveredInADay = robot.UnitsCoveredInADay
            };
        }
    }
}