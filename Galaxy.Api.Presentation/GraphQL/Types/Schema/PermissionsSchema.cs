using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class PermissionsSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<PermissionQueries>(
                "permissions",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<PermissionMutations>(
                "permissions",
                resolve: context => new { });
        }
    }
}