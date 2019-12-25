using System;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Exploration;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class ExplorationMutations : ObjectGraphType
    {
        public ExplorationMutations(ICrudService<Exploration> explorationService, ILogger<ExplorationMutations> logger)
        {
            FieldAsync<ActionResponseViewModel>(
                "create",
                "Add a new exploration to the fleet",
                new QueryArguments(new QueryArgument<ExplorationCreateViewModel> {Name = "exploration"}),
                async context =>
                {
                    try
                    {
                        var model = context.GetArgument<Exploration>("exploration");
                        return await explorationService.AddAsync(model);
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return ActionResponse.ServerError();
                    }
                }).AuthorizeWith(UserPermission.Exploration.ToString());
        }
    }
}