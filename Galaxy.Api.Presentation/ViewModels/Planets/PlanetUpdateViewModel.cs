using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Planets
{
    public class PlanetUpdateViewModel : InputObjectGraphType<Core.Models.Planets.Planet>
    {
        public PlanetUpdateViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Status, true);
            Field(x => x.Description, true);
            Field(x => x.ImagePath,true);
        }
    }
}