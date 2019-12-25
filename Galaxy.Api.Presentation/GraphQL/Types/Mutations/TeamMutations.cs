using System;
using System.Collections.Generic;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Teams;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class TeamMutations : ObjectGraphType
    {
        public TeamMutations(ICrudService<Team> teamService, ILogger<TeamMutations> logger)
        {
            FieldAsync<ActionResponseViewModel>(
                "create",
                "Add a new team",
                new QueryArguments(new QueryArgument<TeamCreateViewModel> {Name = "team"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<Team>("team");
                        return await teamService.AddAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Team.ToString());

            FieldAsync<ActionResponseViewModel>(
                "update",
                "Update team",
                new QueryArguments(new QueryArgument<TeamUpdateViewModel> {Name = "team"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<Dictionary<string, object>>("team");
                        return await teamService.UpdateAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Team.ToString());
        }
    }
}