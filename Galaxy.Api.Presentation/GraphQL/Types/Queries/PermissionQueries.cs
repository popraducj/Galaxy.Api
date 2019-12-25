using System;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Presentation.ViewModels.Permissions;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class PermissionQueries : ObjectGraphType
    {
        public PermissionQueries(IPermissionService permissionService, ILogger<PermissionQueries> logger)
        {
            FieldAsync<ListGraphType<PermissionQueryViewModel>>(
                "getById",
                "Get all permission for the user",
                new QueryArguments(new QueryArgument<IntGraphType>{Name= "userId"}),
                async context =>
                {
                    try
                    {
                        var userId = context.GetArgument<int>("userId");
                        return await permissionService.GetPermissions(userId);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return null;
                    }
                }).AuthorizeWith(UserPermission.Permissions.ToString());
        }
    }
}