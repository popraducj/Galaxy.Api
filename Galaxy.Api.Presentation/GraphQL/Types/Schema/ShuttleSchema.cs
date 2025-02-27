﻿using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class ShuttleSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<ShuttleQueries>(
                "shuttles",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<ShuttleMutations>(
                "shuttles",
                resolve: context => new { });
        }
    }
}