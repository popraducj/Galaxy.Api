using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels
{
    public class ActionErrorViewModel : ObjectGraphType<ActionError>
    {
        public ActionErrorViewModel()
        {
            Field(x => x.Code).Description("Error code");
            Field(x => x.Description).Description("Error description");
        }
    }
}