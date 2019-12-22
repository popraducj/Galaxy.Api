using System;
using Galaxy.Api.Core.Enums;

namespace Galaxy.Api.Core.Models.Teams
{
    public class Shuttle
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int FuelConsumption { get; set; }
        public int FuelTankLimit { get; set; }
        public DateTime NextRevision { get; set; }
        public ShuttleStatus Status { get; set; }
    }
}