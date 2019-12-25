using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Robots;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class RobotQueries: ObjectGraphType
    {
        public RobotQueries(ICrudService<Robot> robotService, ILogger<RobotQueries> logger)
        {
            FieldAsync<ListGraphType<RobotQueryViewModel>>(
                "getAll",
                "Use this to get all the robots",
                new QueryArguments(),
                async context =>
                {
                    try
                    {
                        return await robotService.GetAllAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return null;
                    }
                });
            
            FieldAsync<RobotQueryViewModel>(
                "getById",
                "Use this to get robot info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "robotId"}),
                async context =>
                {
                    try
                    {
                        var robotId = context.GetArgument<Guid>("robotId");
                        return await robotService.GetByIdAsync(robotId);
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