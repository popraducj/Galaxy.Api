using Galaxy.Api.Core.Models.Teams;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Captains
{
    public class CaptainQueryViewModel : ObjectGraphType<Captain>
    {
        public CaptainQueryViewModel()
        {
            Field(x => x.Id).Description("The id of th captain");
            Field(x => x.Name).Description("The name of the captain");
            Field(x => x.Age).Description("The age of the captain");
            Field(x => x.Status).Description("The status of the captain");
            Field(x => x.Expeditions).Description("The number of expeditions the captain took");
            
        }
    }
}