using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Teams
{
    public class TeamUpdateViewModel : InputObjectGraphType<Team>
    {
        public TeamUpdateViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Status);
            Field(x => x.CaptainId, true).Description("The id of the captain");
            Field(x => x.Robots, true, typeof(ListGraphType<GuidGraphTypeCustom>)).Description("A list of id of robots");
            Field(x => x.ShuttleId, true).Description("The shuttle id");
        }
    }
}