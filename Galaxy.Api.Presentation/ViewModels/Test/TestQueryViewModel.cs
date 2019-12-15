using Galaxy.Api.Core.Models;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Test
{
    public class TestQueryViewModel: ObjectGraphType<TestModel>
    {
        public TestQueryViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Name, false).AuthorizeWith(UserPermission.AddCaptain.ToString());
        }
    }
}