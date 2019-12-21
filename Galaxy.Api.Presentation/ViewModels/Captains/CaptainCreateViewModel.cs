using Galaxy.Api.Core.Models.Teams;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Captains
{
    public class CaptainCreateViewModel: InputObjectGraphType<Captain>
    {
        public CaptainCreateViewModel()
        {
            Field(x => x.Age).Description("The age of the captain");
            Field(x => x.Username).Description("The username of the user register");
        }
    }
}