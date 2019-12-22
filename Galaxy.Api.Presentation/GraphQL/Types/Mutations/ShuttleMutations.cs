using System.Collections.Generic;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.ViewModels;
using Galaxy.Api.Presentation.ViewModels.Shuttles;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class ShuttleMutations : ObjectGraphType
    {
        public ShuttleMutations(ICrudService<Shuttle> shuttleService)
        {
            FieldAsync<ActionResponseViewModel>(
                "create",
                "Add a new shuttle",
                new QueryArguments(new QueryArgument<ShuttleCreateViewModel> {Name = "shuttle"}),
                async context =>
                {
                    var model = context.GetArgument<Shuttle>("shuttle");
                    return await shuttleService.AddAsync(model);
                }).AuthorizeWith(UserPermission.AddShuttle.ToString());

            FieldAsync<ActionResponseViewModel>(
                "update",
                "Update shuttle",
                new QueryArguments(new QueryArgument<ShuttleUpdateViewModel> {Name = "shuttle"}),
                async context =>
                {
                    var model = context.GetArgument<Dictionary<string, object>>("shuttle");
                    return await shuttleService.UpdateAsync(model);
                }).AuthorizeWith(UserPermission.AddShuttle.ToString());
        }
    }
}