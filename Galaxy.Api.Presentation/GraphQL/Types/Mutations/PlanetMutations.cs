using System.Collections.Generic;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Planets;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class PlanetMutations : ObjectGraphType
    {
        public PlanetMutations(ICrudService<Planet> planetService)
        {
            FieldAsync<ActionResponseViewModel>(
                "create",
                "Add a new planet",
                new QueryArguments(new QueryArgument<PlanetCreateViewModel> {Name = "planet"}),
                async context =>
                {
                    var model = context.GetArgument<Planet>("planet");
                    return await planetService.AddAsync(model);
                }).AuthorizeWith(UserPermission.AddPlanet.ToString());

            FieldAsync<ActionResponseViewModel>(
                "update",
                "Update planet",
                new QueryArguments(new QueryArgument<PlanetUpdateViewModel> {Name = "planet"}),
                async context =>
                {
                    var model = context.GetArgument<Dictionary<string, object>>("planet");
                    return await planetService.UpdateAsync(model);
                }).AuthorizeWith(UserPermission.AddPlanet.ToString());
        }
    }
}