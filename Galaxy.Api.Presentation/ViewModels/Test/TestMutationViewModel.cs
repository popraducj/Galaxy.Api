using Galaxy.Api.Core.Models;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Test
{
    public class TestMutationViewModel : InputObjectGraphType<TestModel>
    {
        public TestMutationViewModel()
        {
            Field(x => x.Id);
            Field(x => x.Name, false).AuthorizeWith(UserPermission.AddCaptain.ToString());
        }
    }

    public class TestModel
    {
        public string Name { get; set; }    
        public int Id { get; set; } 
    }
}