using Galaxy.Api.Presentation.GraphQl.RootSchema;

namespace Galaxy.Api.Presentation.GraphQl.Types.Schema
{
    public interface ISchemaGroup
    {
        void SetGroup(RootQuery query);
        void SetGroup(RootMutation mutation);
    }
}