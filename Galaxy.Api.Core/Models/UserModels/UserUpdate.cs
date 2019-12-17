using System.ComponentModel.DataAnnotations;

namespace Galaxy.Api.Core.Models.UserModels
{
    public class UserUpdate
    {
        [Required]
        public string Username { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}