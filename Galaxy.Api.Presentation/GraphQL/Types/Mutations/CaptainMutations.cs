﻿using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Presentation.GraphQl.Types.Queries;
using Galaxy.Api.Presentation.ViewModels.Captains;
using Galaxy.Api.Presentation.ViewModels.Users;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.Types.Mutations
{
    public class CaptainMutations : ObjectGraphType
    {
        public CaptainMutations(ICaptainService captainService)
        {
            FieldAsync<UserActionResponseViewModel>(
                "create",
                "Add a new captain to the fleet",
                new QueryArguments(new QueryArgument<CaptainCreateViewModel> {Name = "captain"}),
                async context =>
                {
                    var model = context.GetArgument<Captain>("captain");
                    return await captainService.AddAsync(model);
                }).AuthorizeWith(UserPermission.AddCaptain.ToString());
            
            FieldAsync<UserActionResponseViewModel>(
                "update",
                "Update captain",
                new QueryArguments(new QueryArgument<CaptainUpdateViewModel> {Name = "captain"}),
                async context =>
                {
                    var model = context.GetArgument<Captain>("captain");
                    return await captainService.UpdateAsync(model);
                }).AuthorizeWith(UserPermission.AddCaptain.ToString());;
        }
    }
}