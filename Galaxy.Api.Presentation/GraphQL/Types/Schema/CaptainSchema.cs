using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class CaptainSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<CaptainQueries>(
                "captain",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<CaptainMutations>(
                "captain",
                resolve: context => new { });
        }
    }
}