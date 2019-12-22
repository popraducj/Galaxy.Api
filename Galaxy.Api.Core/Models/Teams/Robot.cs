using System;
using Galaxy.Api.Core.Enums;

namespace Galaxy.Api.Core.Models.Teams
{
    public class Robot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RobotStatus Status { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int UnitsCoveredInADay { get; set; }
        public int TrustWorthyPercentage { get; set; }
        public int FuelConsumptionPerDay { get; set; }
        public DateTime NextRevision { get; set; }
    }
}