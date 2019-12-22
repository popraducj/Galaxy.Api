using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels
{
    public class ActionResponseViewModel : ObjectGraphType<Core.Models.UserModels.ActionResponse>
    {
        public ActionResponseViewModel()
        {
            Field(x => x.Success).Description("True if the action finished successfully");
            Field(x => x.Errors, true, typeof(ListGraphType<ActionErrorViewModel>));
        }
    }
}