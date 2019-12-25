using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Exploration;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class ExplorationQueries : ObjectGraphType
    {
        public ExplorationQueries(ICrudService<Exploration> explorationService, ILogger<ExplorationQueries> logger)
        {
            FieldAsync<ListGraphType<ExplorationQueryViewModel>>(
                "getAll",
                "Use this to get all the explorations",
                new QueryArguments(),
                async context =>
                {
                    try
                    {
                        return await explorationService.GetAllAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return null;
                    }
                });
            
            FieldAsync<ExplorationQueryViewModel>(
                "getById",
                "Use this to get exploration info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "explorationId"}),
                async context =>
                {
                    try
                    {
                        var explorationId = context.GetArgument<Guid>("explorationId");
                        return await explorationService.GetByIdAsync(explorationId);
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