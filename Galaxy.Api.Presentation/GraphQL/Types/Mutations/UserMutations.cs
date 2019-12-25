using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.Authorization;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Users;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.Logging;
using ActionResponseViewModel = Galaxy.Api.Presentation.ViewModels.ActionResponseViewModel;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class UserMutations: ObjectGraphType
    {
        public UserMutations(IUserService userService, ILogger<UserMutations> logger)
        {
            FieldAsync<ActionResponseViewModel>(
                "register",
                "Register new user in the platform",
                new QueryArguments(new QueryArgument<UserRegisterViewModel> {Name = "register"}),
                async context =>
                {
                    try{
                    var model = context.GetArgument<UserRegister>("register");
                    return await userService.RegisterAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                });
            FieldAsync<ActionResponseViewModel>(
                "changePassword",
                "User change password",
                new QueryArguments(new QueryArgument<UserChangePasswordViewModel> {Name = "password"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<UserChangePassword>("password");
                        var userContext = context.UserContext.As<GraphQLUserContext>();

                        model.Username = userContext.UserId.ToString();
                        return await userService.ChangePasswordAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                });
            FieldAsync<ActionResponseViewModel>(
                "update",
                "Update user profile information",
                new QueryArguments(new QueryArgument<UserUpdateViewModel> {Name = "profile"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<UserUpdate>("profile");
                        var userContext = context.UserContext.As<GraphQLUserContext>();

                        model.Username = userContext.UserId.ToString();
                        return await userService.UpdateAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                });
        }
    }
}