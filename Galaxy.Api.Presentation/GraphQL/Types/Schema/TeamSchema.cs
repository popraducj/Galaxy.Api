﻿using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class TeamSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<TeamQueries>(
                "teams",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<TeamMutations>(
                "teams",
                resolve: context => new { });
        }
    }
}