using System.Collections.Generic;
using Galaxy.Api.Presentation.GraphQl.Types.Schema;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.RootSchema
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation(IEnumerable<ISchemaGroup> mutations)
        {
            Name = "Mutation";
            foreach (var mutation in mutations)
            {
                mutation.SetGroup(this);
            }
        }
    }
}