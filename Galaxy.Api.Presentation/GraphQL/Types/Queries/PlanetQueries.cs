using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Planets;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class PlanetQueries : ObjectGraphType
    {
        public PlanetQueries(ICrudService<Planet> planetService)
        {
            FieldAsync<ListGraphType<PlanetQueryViewModel>>(
                "getAll",
                "Use this to get all the planets",
                new QueryArguments(),
                async context => await planetService.GetAllAsync());
            
            FieldAsync<PlanetQueryViewModel>(
                "getById",
                "Use this to get planet info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "planetId"}),
                async context =>
                {
                    var planetId = context.GetArgument<Guid>("planetId");
                    return await planetService.GetByIdAsync(planetId);
                });
        }
    }
}