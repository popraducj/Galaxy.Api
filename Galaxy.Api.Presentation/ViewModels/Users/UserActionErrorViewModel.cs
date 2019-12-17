using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Users
{
    public class UserActionErrorViewModel : ObjectGraphType<UserActionError>
    {
        public UserActionErrorViewModel()
        {
            Field(x => x.Code).Description("Error code");
            Field(x => x.Description).Description("Error description");
        }
    }
}