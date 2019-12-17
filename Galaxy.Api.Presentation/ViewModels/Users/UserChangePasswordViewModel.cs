using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Users
{
    public class UserChangePasswordViewModel : InputObjectGraphType<UserChangePassword>
    {
        public UserChangePasswordViewModel()
        {
            Field(x => x.OldPassword).Description("User old password");
            Field(x => x.NewPassword).Description("User new password");
            Field(x => x.ConfirmNewPassword).Description("User confirmed new password");
        }
    }
}