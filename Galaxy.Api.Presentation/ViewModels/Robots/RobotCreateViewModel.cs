using Galaxy.Api.Core.Models.Teams;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Robots
{
    public class RobotCreateViewModel : InputObjectGraphType<Robot>
    {
        public RobotCreateViewModel()
        {
            Field(x => x.Name);
            Field(x => x.Manufacturer).Description("The company who produced the robot");
            Field(x => x.Model, true).Description("The model of the robot");
            Field(x => x.Year).Description("The year in which the robot was made");
            Field(x => x.TrustWorthyPercentage).Description("A percentage of how much a robot can be trusted");
            Field(x => x.FuelConsumptionPerDay).Description("How much a robot consumes per day");
            Field(x => x.UnitsCoveredInADay).Description("How much ground a robot can cover in a day");
            Field(x => x.NextRevision).Description("Next time the robot needs to visit a service");
        }
    }
}