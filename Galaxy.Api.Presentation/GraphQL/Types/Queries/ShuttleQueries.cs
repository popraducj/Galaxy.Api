using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Shuttles;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class ShuttleQueries: ObjectGraphType
    {
        public ShuttleQueries(ICrudService<Shuttle> shuttleService)
        {
            FieldAsync<ListGraphType<ShuttleQueryViewModel>>(
                "getAll",
                "Use this to get all the shuttles",
                new QueryArguments(),
                async context => await shuttleService.GetAllAsync());
            
            FieldAsync<ShuttleQueryViewModel>(
                "getById",
                "Use this to get shuttle info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "shuttleId"}),
                async context =>
                {
                    var shuttleId = context.GetArgument<Guid>("shuttleId");
                    return await shuttleService.GetByIdAsync(shuttleId);
                });
        }
    }
}