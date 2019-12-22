using System;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using Galaxy.Api.Presentation.ViewModels.Teams;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Queries
{
    public class TeamQueries : ObjectGraphType
    {
        public TeamQueries(ICrudService<Team> teamService)
        {
            FieldAsync<ListGraphType<TeamQueryViewModel>>(
                "getAll",
                "Use this to get all the teams",
                new QueryArguments(),
                async context => await teamService.GetAllAsync());
            
            FieldAsync<TeamQueryViewModel>(
                "getById",
                "Use this to get team info",
                new QueryArguments(new QueryArgument<GuidGraphTypeCustom>{Name= "teamId"}),
                async context =>
                {
                    var teamId = context.GetArgument<Guid>("teamId");
                    return await teamService.GetByIdAsync(teamId);
                });
        }
    }
}