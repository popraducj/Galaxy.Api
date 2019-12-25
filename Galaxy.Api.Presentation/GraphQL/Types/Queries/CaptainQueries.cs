using System;
using Galaxy.Api.Core.Helpers;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Captains;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class CaptainQueries : ObjectGraphType
    {
        public CaptainQueries(ICrudService<Captain> captainService, ILogger<CaptainQueries> logger)
        {
            FieldAsync<ListGraphType<CaptainQueryViewModel>>(
                "getAll",
                "Use this to get all the captains",
                new QueryArguments(),
                async context =>
                {
                    try
                    {
                        return await captainService.GetAllAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return null;
                    }
                });
            
            FieldAsync<CaptainQueryViewModel>(
                "getById",
                "Use this to get captain info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "captainId"}),
                async context =>
                {
                    try{
                        var captainId = context.GetArgument<Guid>("captainId");
                        return await captainService.GetByIdAsync(captainId);
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