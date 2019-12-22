using System;
using Galaxy.Api.Core.Enums;
using Galaxy.Shuttles.Presentation;
using Shuttle = Galaxy.Api.Core.Models.Teams.Shuttle;

namespace Galaxy.Api.Infrastructure.Helpers
{
    public static class ShuttleExtensions
    {
        public static ShuttleModel ToShuttleModel(this Shuttle shuttle)
        {
            return new ShuttleModel
            {
                Id = shuttle.Id.ToString(),
                Name = shuttle.Name ?? string.Empty,
                Status = (int) shuttle.Status,
                Manufacturer = shuttle.Manufacturer ?? string.Empty,
                Model = shuttle.Model ?? string.Empty,
                Year = shuttle.Year,
                NextRevision = shuttle.NextRevision.ToString("s"),
                FuelConsumption = shuttle.FuelConsumption,
                MaxSpeed = shuttle.MaxSpeed,
                FuelTankLimit = shuttle.FuelTankLimit
            };
        }
        public static Shuttle ToShuttle(this ShuttleModel shuttle)
        {
            return new Shuttle
            {
                Id = Guid.Parse(shuttle.Id),
                Name = shuttle.Name,
                Status = (ShuttleStatus) shuttle.Status,
                Manufacturer = shuttle.Manufacturer,
                Model = shuttle.Model,
                Year = shuttle.Year,
                NextRevision = DateTime.Parse(shuttle.NextRevision),
                FuelConsumption = shuttle.FuelConsumption,
                MaxSpeed = shuttle.MaxSpeed,
                FuelTankLimit = shuttle.FuelTankLimit
            };
        }
    }
}