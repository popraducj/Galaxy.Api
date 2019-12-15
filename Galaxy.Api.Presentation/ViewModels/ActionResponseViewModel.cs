using Galaxy.Api.Core.Models;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels
{
    public class ActionResponseViewModel : ObjectGraphType<ActionResponse>
    {
        public ActionResponseViewModel()
        {
            Field(x => x.Success);
        }
    }

    public class ActionResponse
    {
        public bool Success { get; set; }
    }
}