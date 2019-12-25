using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.ViewModels.Users;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class UserQueries: ObjectGraphType
    {
        public UserQueries(IUserService userService, ILogger<UserQueries> logger)
        {
            FieldAsync<UserLoginActionResponseViewModel>(
                "login",
                "Use this to get access token",
                new QueryArguments(new QueryArgument<UserLoginViewModel> {Name = "login"}),
                async context =>
                {
                    try
                    {
                        var loginModel = context.GetArgument<UserLogin>("login");
                        return await userService.LoginAsync(loginModel);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return null;
                    }
                });
        }
    }
}