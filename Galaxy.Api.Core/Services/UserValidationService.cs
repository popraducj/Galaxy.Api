using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Api.Core.Services
{
    public class UserValidationService :IUserValidationService
    {
        public async Task ValidateAsync(TokenValidatedContext ctx)
        {
            var userClientGrpcService = ctx.HttpContext.RequestServices.GetRequiredService<IUserClientGrpcService>();
            var permissionClientGrpcService = ctx.HttpContext.RequestServices.GetRequiredService<IPermissionClientGrpcService>();
            var username = ctx.Principal.Identity.Name;
            var userId = await userClientGrpcService.VerifyIfUserExistsAsync(username);
            if (userId == 0)
            {
                throw new UnauthorizedAccessException();
            }

            var permissions = await permissionClientGrpcService.GetPermissions(userId);
            var claims = new List<Claim>();
            permissions.ForEach(x =>
            {
                claims.Add(new Claim(Enum.GetName(typeof(UserPermission), x), "true"));
            });
            ctx.Principal.AddIdentity(new ClaimsIdentity(claims));
        }
    }
}