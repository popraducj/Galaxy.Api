using Galaxy.Api.Presentation.GraphQl.RootSchema;
using Galaxy.Api.Presentation.GraphQl.Types.Mutations;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public class UserSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<UserQueries>(
                "user",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<UserMutations>(
                "user",
                resolve: context => new { });
        }
    }
}