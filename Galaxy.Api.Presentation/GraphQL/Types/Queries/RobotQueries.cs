using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Robots;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class RobotQueries: ObjectGraphType
    {
        public RobotQueries(ICrudService<Robot> robotService)
        {
            FieldAsync<ListGraphType<RobotQueryViewModel>>(
                "getAll",
                "Use this to get all the robots",
                new QueryArguments(),
                async context => await robotService.GetAllAsync());
            
            FieldAsync<RobotQueryViewModel>(
                "getById",
                "Use this to get robot info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "robotId"}),
                async context =>
                {
                    var robotId = context.GetArgument<Guid>("robotId");
                    return await robotService.GetByIdAsync(robotId);
                });
        }
    }
}