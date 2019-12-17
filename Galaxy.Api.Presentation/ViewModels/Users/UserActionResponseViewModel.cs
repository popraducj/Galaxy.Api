using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Language.AST;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Users
{
    public class UserActionResponseViewModel : ObjectGraphType<UserActionResponse>
    {
        public UserActionResponseViewModel()
        {
            Field(x => x.Success).Description("True if the action finished successfully");
            Field(x => x.Errors, true, typeof(ListGraphType<UserActionErrorViewModel>));
        }
    }
}