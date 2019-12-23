using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Planets
{
    public class PlanetQueryViewModel : ObjectGraphType<Core.Models.Planets.Planet>
    {
        public PlanetQueryViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Status);
            Field(x => x.Description);
            Field(x => x.Units);
            Field(x => x.ImagePath);
        }
    }
}