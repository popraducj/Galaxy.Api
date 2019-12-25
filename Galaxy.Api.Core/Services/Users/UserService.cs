using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.UserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Api.Core.Services.Users
{
    public class UserService :IUserService
    {
        private readonly IUserClientGrpcService _userClientGrpcService;

        public UserService(IUserClientGrpcService userClientGrpcService)
        {
            _userClientGrpcService = userClientGrpcService;
        }
        public async Task ValidateAsync(TokenValidatedContext ctx)
        {
            var userClientGrpcService = ctx.HttpContext.RequestServices.GetRequiredService<IUserClientGrpcService>();
            var permissionClientGrpcService = ctx.HttpContext.RequestServices.GetRequiredService<IPermissionClientGrpcService>();
            var username = ctx.Principal.Identity.Name;
            var userId = await userClientGrpcService.GetUserIdAsync(username);
            if (userId == 0)
            {
                throw new UnauthorizedAccessException();
            }

            var permissions = await permissionClientGrpcService.GetPermissionsAsync(userId);
            var claims = new List<Claim>();
            permissions.ForEach(x =>
            {
                claims.Add(new Claim(Enum.GetName(typeof(UserPermission), x), "true"));
            });
            ctx.Principal.AddIdentity(new ClaimsIdentity(claims));
        }
        
        public async Task<ActionResponse> ActivateAsync(string token)
        {
            return await _userClientGrpcService.ActivateAsync(token);
        }
        
        public async Task<ActionResponse> RegisterAsync(UserRegister model)
        {
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                return new ActionResponse
                {
                    Success = false,
                    Errors = new List<ActionError>
                    {
                        new ActionError
                        {
                            Code = "PasswordsDoNotMatch",
                            Description ="Please make sure the password and confirm password have same value" 
                        }
                    }
                };
            }
            return await _userClientGrpcService.RegisterAsync(model);
        }
        
        public async Task<ActionResponse> UpdateAsync(UserUpdate model)
        {
            return await _userClientGrpcService.UpdateAsync(model);
        }

        public async Task<ActionResponse> ChangePasswordAsync(UserChangePassword model)
        {
            if (!model.NewPassword.Equals(model.ConfirmNewPassword))
            {
                return new ActionResponse
                {
                    Success = false,
                    Errors = new List<ActionError>
                    {
                        new ActionError
                        {
                            Code = "PasswordsDoNotMatch",
                            Description ="Please make sure the new password and confirm new password have same value" 
                        }
                    }
                };
            }
            return await _userClientGrpcService.ChangePasswordAsync(model);
        }
        
        public async Task<LoginActionResponse> LoginAsync(UserLogin model)
        {
            var response = new LoginActionResponse();
            try
            {
                response.Token = await _userClientGrpcService.LoginAsync(model);
                response.Success = true;
            }
            catch
            {
                response.Success = false;
                response.Errors = new List<ActionError>
                {
                    new ActionError
                    {
                        Code = "FailedLogin",
                        Description = "Login has failed. The email and password do not match"
                    }
                };
            }

            return response;
        }
    }
}