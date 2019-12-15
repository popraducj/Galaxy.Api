using System.Collections.Generic;
using Galaxy.Api.Presentation.GraphQl.Types.Schema;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.RootSchema
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IEnumerable<ISchemaGroup> queries)
        {
            Name = "Query";
            foreach (var query in queries)
            {
                query.SetGroup(this);
            }
        }
    }
}