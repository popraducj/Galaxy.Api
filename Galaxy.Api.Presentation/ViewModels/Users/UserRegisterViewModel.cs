using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Users
{
    public class UserRegisterViewModel: InputObjectGraphType<UserRegister>
    {
        public UserRegisterViewModel()
        {
            Field(x => x.Email).Description("The user email address");
            Field(x => x.Name).Description("Users full name");
            Field(x => x.Password).Description("User password");
            Field(x => x.ConfirmPassword).Description("Confirm password");
        }
    }
}