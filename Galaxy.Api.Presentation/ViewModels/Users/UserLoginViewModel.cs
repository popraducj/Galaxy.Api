using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Users
{
    public class UserLoginViewModel : InputObjectGraphType<UserLogin>
    {
        public UserLoginViewModel()
        {
            Field(x => x.Email).Description("The email address with which user has ");
            Field(x => x.Password).Description("User password");
        }
    }
}