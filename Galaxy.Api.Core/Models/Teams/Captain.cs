using Galaxy.Api.Core.Enums;

namespace Galaxy.Api.Core.Models.Teams
{
    public class Captain
    {
        public int Age { get; set; }
        public string Username { get; set; }
        public CaptainStatus Status { get; set; }
        public int Expeditions { get; set; }
    }
}