using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Teams;
using GraphQL.Types;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class TeamQueries : ObjectGraphType
    {
        public TeamQueries(ICrudService<Team> teamService, ILogger<TeamQueries>  logger)
        {
            FieldAsync<ListGraphType<TeamQueryViewModel>>(
                "getAll",
                "Use this to get all the teams",
                new QueryArguments(),
                async context =>
                {
                    try
                    {
                        return await teamService.GetAllAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.LogCritical(ex.Message, ex);
                        return null;
                    }
                });
            
            FieldAsync<TeamQueryViewModel>(
                "getById",
                "Use this to get team info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "teamId"}),
                async context =>
                {
                    try
                    {
                        var teamId = context.GetArgument<Guid>("teamId");
                        return await teamService.GetByIdAsync(teamId);
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