using Galaxy.Api.Core.Models.Teams;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Robots
{
    public class RobotUpdateViewModel : InputObjectGraphType<Robot>
    {
        public RobotUpdateViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Name,true);
            Field(x => x.Status,true);
            Field(x => x.TrustWorthyPercentage,true).Description("A percentage of how much a robot can be trusted");
            Field(x => x.FuelConsumptionPerDay,true).Description("How much a robot consumes per day");
            Field(x => x.UnitsCoveredInADay,true).Description("How much ground a robot can cover in a day");
            Field(x => x.NextRevision,true).Description("Next time the robot needs to visit a service");
        }
    }
}