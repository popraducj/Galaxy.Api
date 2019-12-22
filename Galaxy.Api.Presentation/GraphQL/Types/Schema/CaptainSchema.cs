﻿using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class CaptainSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<CaptainQueries>(
                "captains",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<CaptainMutations>(
                "captains",
                resolve: context => new { });
        }
    }
}