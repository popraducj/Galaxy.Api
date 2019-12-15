using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class Test : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<TestQueries>(
                "test",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<TestMutations>(
                "test",
                resolve: context => new { });
        }
    }
}