using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Captains;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class CaptainQueries : ObjectGraphType
    {
        public CaptainQueries(ICaptainService captainService)
        {
            FieldAsync<ListGraphType<CaptainQueryViewModel>>(
                "getAll",
                "Use this to all the captains",
                new QueryArguments(),
                async context => await captainService.GetAllAsync());
            
            FieldAsync<CaptainQueryViewModel>(
                "getById",
                "Use this to get captain info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "captainId"}),
                async context =>
                {
                    var captainId = context.GetArgument<Guid>("captainId");
                    return await captainService.GetByIdAsync(captainId);
                });
        }
    }
}