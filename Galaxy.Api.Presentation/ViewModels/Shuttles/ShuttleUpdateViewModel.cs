using Galaxy.Api.Core.Models.Teams;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Shuttles
{
    public class ShuttleUpdateViewModel : InputObjectGraphType<Shuttle>
    {
        public ShuttleUpdateViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Name, true);
            Field(x => x.Status, true);
            Field(x => x.FuelConsumption, true).Description("how much a the shuttle consumes");
            Field(x => x.MaxSpeed, true).Description("The max speed at which the shuttle can cruise");
            Field(x => x.FuelTankLimit, true).Description("How much fuel the shuttle can have in tank");
            Field(x => x.NextRevision, true).Description("Next time the shuttle needs to visit a service");
        }
    }
}