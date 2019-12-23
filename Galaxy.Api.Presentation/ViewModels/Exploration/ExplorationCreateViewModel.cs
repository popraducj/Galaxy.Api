using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Exploration
{
    public class ExplorationCreateViewModel : InputObjectGraphType<Core.Models.Planets.Exploration>
    {
        public ExplorationCreateViewModel()
        {
            Field(x => x.Name);
            Field(x => x.PlanetId);
            Field(x => x.TeamId);
        }
    }
}