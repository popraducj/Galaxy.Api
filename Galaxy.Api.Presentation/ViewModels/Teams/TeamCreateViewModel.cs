using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Teams
{
    public class TeamCreateViewModel : InputObjectGraphType<Team>
    {
        public TeamCreateViewModel()
        {
            Field(x => x.Name);
            Field(x => x.CaptainId).Description("The id of the captain");
            Field(x => x.Robots, false, typeof(ListGraphType<GuidGraphTypeCustom>)).Description("A list of id of robots");
            Field(x => x.ShuttleId).Description("The shuttle id");
        }
    }
}