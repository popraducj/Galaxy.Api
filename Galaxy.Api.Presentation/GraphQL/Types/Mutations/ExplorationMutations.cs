using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Exploration;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class ExplorationMutations : ObjectGraphType
    {
        public ExplorationMutations(ICrudService<Exploration> explorationService)
        {
            FieldAsync<ActionResponseViewModel>(
                "create",
                "Add a new exploration to the fleet",
                new QueryArguments(new QueryArgument<ExplorationCreateViewModel> {Name = "exploration"}),
                async context =>
                {
                    var model = context.GetArgument<Exploration>("exploration");
                    return await explorationService.AddAsync(model);
                }).AuthorizeWith(UserPermission.AddExploration.ToString());
        }
    }
}