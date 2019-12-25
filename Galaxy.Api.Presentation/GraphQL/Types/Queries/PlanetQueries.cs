using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Planets;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class PlanetQueries : ObjectGraphType
    {
        public PlanetQueries(ICrudService<Planet> planetService, ILogger<PlanetQueries> logger)
        {
            FieldAsync<ListGraphType<PlanetQueryViewModel>>(
                "getAll",
                "Use this to get all the planets",
                new QueryArguments(),
                async context =>
                {
                    try
                    {
                        return await planetService.GetAllAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return null;
                    }
                });
            
            FieldAsync<PlanetQueryViewModel>(
                "getById",
                "Use this to get planet info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "planetId"}),
                async context =>
                {
                    try
                    {
                        var planetId = context.GetArgument<Guid>("planetId");
                        return await planetService.GetByIdAsync(planetId);
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