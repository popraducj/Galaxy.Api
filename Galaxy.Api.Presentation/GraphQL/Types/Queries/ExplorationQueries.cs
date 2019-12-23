using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Exploration;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class ExplorationQueries : ObjectGraphType
    {
        public ExplorationQueries(ICrudService<Exploration> explorationService)
        {
            FieldAsync<ListGraphType<ExplorationQueryViewModel>>(
                "getAll",
                "Use this to get all the explorations",
                new QueryArguments(),
                async context => await explorationService.GetAllAsync());
            
            FieldAsync<ExplorationQueryViewModel>(
                "getById",
                "Use this to get exploration info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "explorationId"}),
                async context =>
                {
                    var explorationId = context.GetArgument<Guid>("explorationId");
                    return await explorationService.GetByIdAsync(explorationId);
                });
        }
    }
}