using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class PlanetSchema: ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<PlanetQueries>(
                "planets",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<PlanetMutations>(
                "planets",
                resolve: context => new { });
        }
    }
}