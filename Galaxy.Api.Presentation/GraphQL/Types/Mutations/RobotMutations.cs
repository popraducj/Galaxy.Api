using System;
using System.Collections.Generic;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Robots;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class RobotMutations : ObjectGraphType
    {
        public RobotMutations(ICrudService<Robot> robotService)
        {
            FieldAsync<ActionResponseViewModel>(
                "create",
                "Add a new robot",
                new QueryArguments(new QueryArgument<RobotCreateViewModel> {Name = "robot"}),
                async context =>
                {
                        var model = context.GetArgument<Robot>("robot");
                        return await robotService.AddAsync(model);
                }).AuthorizeWith(UserPermission.AddRobot.ToString());

            FieldAsync<ActionResponseViewModel>(
                "update",
                "Update robot",
                new QueryArguments(new QueryArgument<RobotUpdateViewModel> {Name = "robot"}),
                async context =>
                {
                    var model = context.GetArgument<Dictionary<string, object>>("robot");
                    return await robotService.UpdateAsync(model);
                }).AuthorizeWith(UserPermission.AddRobot.ToString());
        }
    }
}