using System;
using System.Collections.Generic;
using Galaxy.Api.Core.Enums;

namespace Galaxy.Api.Core.Models.Teams
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TeamStatus Status { get; set; }
        public Guid CaptainId { get; set; }
        public Guid ShuttleId { get; set; }
        public List<Guid> Robots { get; set; }
    }
}