using System.ComponentModel.DataAnnotations;

namespace Galaxy.Api.Core.Models.UserModels
{
    public class UserChangePassword
    {
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}