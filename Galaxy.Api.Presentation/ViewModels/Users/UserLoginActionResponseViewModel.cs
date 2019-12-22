using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Users
{
    public class UserLoginActionResponseViewModel : ObjectGraphType<LoginActionResponse>
    {
        public UserLoginActionResponseViewModel()
        {
            Field(x => x.Success).Description("True if the action finished successfully");
            Field(x => x.Token, true).Description("If login has succeded a token is returned");
            Field(x => x.Errors, true, typeof(ListGraphType<ActionErrorViewModel>));
        }
    }
}