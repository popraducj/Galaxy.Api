using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class RobotSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<RobotQueries>(
                "robots",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<RobotMutations>(
                "robots",
                resolve: context => new { });
        }
    }
}