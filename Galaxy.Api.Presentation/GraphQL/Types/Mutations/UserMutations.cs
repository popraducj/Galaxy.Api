using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.Authorization;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Users;
using GraphQL;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class UserMutations: ObjectGraphType
    {
        public UserMutations(IUserService userService)
        {
            FieldAsync<UserActionResponseViewModel>(
                "register",
                "Register new user in the platform",
                new QueryArguments(new QueryArgument<UserRegisterViewModel> {Name = "register"}),
                async context =>
                {
                    var model = context.GetArgument<UserRegister>("register");
                    return await userService.RegisterAsync(model);
                });
            FieldAsync<UserActionResponseViewModel>(
                "changePassword",
                "User change password",
                new QueryArguments(new QueryArgument<UserChangePasswordViewModel> {Name = "password"}),
                async context =>
                {
                    var model = context.GetArgument<UserChangePassword>("password");
                    var userContext = context.UserContext.As<GraphQLUserContext>();
                    
                    model.Username = userContext.UserId.ToString();
                    return await userService.ChangePasswordAsync(model);
                });
            FieldAsync<UserActionResponseViewModel>(
                "update",
                "Update user profile information",
                new QueryArguments(new QueryArgument<UserUpdateViewModel> {Name = "profile"}),
                async context =>
                {
                    var model = context.GetArgument<UserUpdate>("profile");
                    var userContext = context.UserContext.As<GraphQLUserContext>();
                    
                    model.Username = userContext.UserId.ToString();
                    return await userService.UpdateAsync(model);
                });
        }
    }
}