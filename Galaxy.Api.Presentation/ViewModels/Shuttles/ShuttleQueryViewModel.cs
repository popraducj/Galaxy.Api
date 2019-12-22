using Galaxy.Api.Core.Models.Teams;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Shuttles
{
    public class ShuttleQueryViewModel : ObjectGraphType<Shuttle>
    {
        public ShuttleQueryViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Status);
            Field(x => x.Manufacturer).Description("The company who produced the shuttle");
            Field(x => x.Model, true).Description("The model of the shuttle");
            Field(x => x.Year).Description("The year in which the shuttle was made");
            Field(x => x.FuelConsumption).Description("how much a the shuttle consumes");
            Field(x => x.MaxSpeed).Description("The max speed at which the shuttle can cruise");
            Field(x => x.FuelTankLimit).Description("How much fuel the shuttle can have in tank");
            Field(x => x.NextRevision).Description("Next time the shuttle needs to visit a service");
        }
    }
}