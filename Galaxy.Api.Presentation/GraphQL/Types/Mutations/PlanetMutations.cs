using System;
using System.Collections.Generic;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Planets;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class PlanetMutations : ObjectGraphType
    {
        public PlanetMutations(ICrudService<Planet> planetService, ILogger<PlanetMutations> logger)
        {
            FieldAsync<ActionResponseViewModel>(
                "create",
                "Add a new planet",
                new QueryArguments(new QueryArgument<PlanetCreateViewModel> {Name = "planet"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<Planet>("planet");
                        return await planetService.AddAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Planet.ToString());

            FieldAsync<ActionResponseViewModel>(
                "update",
                "Update planet",
                new QueryArguments(new QueryArgument<PlanetUpdateViewModel> {Name = "planet"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<Dictionary<string, object>>("planet");
                        return await planetService.UpdateAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Planet.ToString());
        }
    }
}