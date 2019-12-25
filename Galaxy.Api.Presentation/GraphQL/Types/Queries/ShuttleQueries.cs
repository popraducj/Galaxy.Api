using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Shuttles;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class ShuttleQueries: ObjectGraphType
    {
        public ShuttleQueries(ICrudService<Shuttle> shuttleService, ILogger<ShuttleQueries> logger)
        {
            FieldAsync<ListGraphType<ShuttleQueryViewModel>>(
                "getAll",
                "Use this to get all the shuttles",
                new QueryArguments(),
                async context =>
                {
                    try
                    {
                        return await shuttleService.GetAllAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return null;
                    }
                });
            
            FieldAsync<ShuttleQueryViewModel>(
                "getById",
                "Use this to get shuttle info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "shuttleId"}),
                async context =>
                {
                    try
                    {
                        var shuttleId = context.GetArgument<Guid>("shuttleId");
                        return await shuttleService.GetByIdAsync(shuttleId);
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