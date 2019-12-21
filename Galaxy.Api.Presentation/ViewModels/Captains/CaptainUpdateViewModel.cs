using Galaxy.Api.Core.Models.Teams;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Captains
{
    public class CaptainUpdateViewModel : InputObjectGraphType<Captain>
    {
        public CaptainUpdateViewModel()
        {
            Field(x => x.Id).Description("The id of the captain");
            Field(x => x.Status).Description("Change the status of the captain");
        }
    }
}