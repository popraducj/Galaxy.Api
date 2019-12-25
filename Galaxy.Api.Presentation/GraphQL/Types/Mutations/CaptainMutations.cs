using System;
using System.Collections.Generic;
using System.Linq;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Helpers;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Captains;
using Galaxy.Api.Presentation.ViewModels.Users;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class CaptainMutations : ObjectGraphType
    {
        public CaptainMutations(ICrudService<Captain> captainService, ILogger<CaptainMutations> logger)
        {
            FieldAsync<ActionResponseViewModel>(
                "create",
                "Add a new captain to the fleet",
                new QueryArguments(new QueryArgument<CaptainCreateViewModel> {Name = "captain"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<Captain>("captain");
                        return await captainService.AddAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Captain.ToString());

            FieldAsync<ActionResponseViewModel>(
                "update",
                "Update captain",
                new QueryArguments(new QueryArgument<CaptainUpdateViewModel> {Name = "captain"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<Dictionary<string, object>>("captain");
                        return await captainService.UpdateAsync(model);
                    }
                    catch(Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Captain.ToString());
        }
    }
}