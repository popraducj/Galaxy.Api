using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class ExplorationSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<ExplorationQueries>(
                "explorations",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<ExplorationMutations>(
                "explorations",
                resolve: context => new { });
        }
    }
}