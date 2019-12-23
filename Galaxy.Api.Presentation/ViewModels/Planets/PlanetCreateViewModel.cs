using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Planets
{
    public class PlanetCreateViewModel : InputObjectGraphType<Core.Models.Planets.Planet>
    {
        public PlanetCreateViewModel()
        {
            Field(x => x.Name);
            Field(x => x.Units);
        }
    }
}