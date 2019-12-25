using System;
using System.Collections.Generic;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.Authorization;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Permissions;
using GraphQL;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class PermissionMutations : ObjectGraphType
    {
        public PermissionMutations(IPermissionService permissionService, ILogger<PermissionMutations> logger)
        {
            FieldAsync<ActionResponseViewModel>(
                "add",
                "Add a new permissions for user",
                new QueryArguments(new QueryArgument<ListGraphType<PermissionInputViewModel>> {Name = "permissions"}),
                async context =>
                {
                    try
                    {
                        var permissions = context.GetArgument<List<Permission>>("permissions");
                        var username = context.UserContext.As<GraphQLUserContext>().UserId.ToString();
                        return await permissionService.AddAsync(permissions, username);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Permissions.ToString());
            
            FieldAsync<ActionResponseViewModel>(
                "remove",
                "Remove permissions for user",
                new QueryArguments(new QueryArgument<ListGraphType<PermissionInputViewModel>> {Name = "permissions"}),
                async context =>
                {
                    
                    try
                    {
                        var permissions = context.GetArgument<List<Permission>>("permissions");
                        var username = context.UserContext.As<GraphQLUserContext>().UserId.ToString();
                        return await permissionService.RemoveAsync(permissions, username);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Permissions.ToString());
        }
    }
}