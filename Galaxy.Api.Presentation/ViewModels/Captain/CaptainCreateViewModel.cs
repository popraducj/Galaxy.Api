using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Captain
{
    public class CaptainCreateViewModel: InputObjectGraphType<Core.Models.Teams.Captain>
    {
        public CaptainCreateViewModel()
        {
            Field(x => x.Age).Description("The age of the captain");
            Field(x => x.Username).Description("The username of the user register");
        }
    }
}