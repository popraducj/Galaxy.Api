using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.ViewModels.Users;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class UserQueries: ObjectGraphType
    {
        public UserQueries(IUserService userService)
        {
            FieldAsync<UserLoginActionResponseViewModel>(
                "login",
                "Use this to get access token",
                new QueryArguments(new QueryArgument<UserLoginViewModel> {Name = "login"}),
                async context =>
                {
                    var loginModel = context.GetArgument<UserLogin>("login");
                    return await userService.LoginAsync(loginModel);
                });
        }
    }
}