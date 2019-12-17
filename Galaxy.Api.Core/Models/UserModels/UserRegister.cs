using System.ComponentModel.DataAnnotations;

namespace Galaxy.Api.Core.Models.UserModels
{
    public class UserRegister
    {
        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(256)] 
        public string Name { get; set; }
        
        [Required] 
        [MaxLength(256)]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(256)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}