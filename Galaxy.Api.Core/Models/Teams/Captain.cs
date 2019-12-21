using System;
using Galaxy.Api.Core.Enums;

namespace Galaxy.Api.Core.Models.Teams
{
    public class Captain
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public CaptainStatus Status { get; set; }
        public int Expeditions { get; set; }
    }
}