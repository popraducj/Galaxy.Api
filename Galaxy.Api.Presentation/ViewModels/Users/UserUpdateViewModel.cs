using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Users
{
    public class UserUpdateViewModel : InputObjectGraphType<UserUpdate>
    {
        public UserUpdateViewModel()
        {
            Field(x => x.Name, true).Description("User new name");
            Field(x => x.Phone, true).Description("User new phone number");
        }
    }
}