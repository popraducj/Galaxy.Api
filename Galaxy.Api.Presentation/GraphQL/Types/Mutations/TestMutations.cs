using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Test;
using GraphQL.Types;
using Microsoft.Extensions.Logging;
using NLog;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class TestMutations : ObjectGraphType
    {
        private const string ArgumentName = "test";
        public TestMutations(ILogger<TestMutations> logger)
        {
            Field<ActionResponseViewModel>(
                "create",
                "some random test",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<TestMutationViewModel>>
                        {Name = ArgumentName, Description = "Account Entity to be Created"}),
                context => new ActionResponse {Success = true});
        }
    }
}