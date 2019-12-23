using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Exploration
{
    public class ExplorationQueryViewModel : ObjectGraphType<Core.Models.Planets.Exploration>
    {
        public ExplorationQueryViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Status);
            Field(x => x.PlanetId);
            Field(x => x.TeamId);
            Field(x => x.RobotsReports);
        }
    }
}