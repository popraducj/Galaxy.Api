using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Infrastructure.Helpers;
using Galaxy.Auth;
using Galaxy.Teams.Core.Models.Settings;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class PermissionsService : IPermissionClientGrpcService
    {
        private readonly Permissions.PermissionsClient _client;
        public PermissionsService(IOptions<AppSettings> appSettings)
        {
            var channel = GrpcChannel.ForAddress(appSettings.Value.Urls.AuthUrl);
            _client = new Permissions.PermissionsClient(channel);
        }

        public async Task<ActionResponse> AddPermissions(IEnumerable<Permission> permissions)
        {
            using var call = _client.AddPermission();
            foreach (var permission in permissions)
            {
                await call.RequestStream.WriteAsync(new PermissionRequest
                {
                    UserId = permission.UserId,
                    Permission = (int) permission.UserPermission
                });
            }
            await call.RequestStream.CompleteAsync();
            var response = await call.ResponseAsync;
            return response.ToActionResponse();
        }

        public async Task<ActionResponse> RemovePermissions(IEnumerable<Permission> permissions)
        {
            using var call = _client.RemovePermission();
            foreach (var permission in permissions)
            {
                await call.RequestStream.WriteAsync(new PermissionRequest
                {
                    UserId = permission.UserId,
                    Permission = (int) permission.UserPermission
                });
            }
            await call.RequestStream.CompleteAsync();
            var response = await call.ResponseAsync;
            return response.ToActionResponse();
        }
        
        public async Task<List<UserPermission>> GetPermissionsAsync(int userId)
        {
            var permissions = new List<UserPermission>();
            using (var call = _client.GetPermissions(new UserPermissionRequest{Id = userId}))
            {
                var responseStream = call.ResponseStream;
                await foreach (var permission in responseStream.ReadAllAsync())
                {
                    if(!string.IsNullOrEmpty(Enum.GetName(typeof(UserPermission), permission.Permission)))
                        permissions.Add((UserPermission)permission.Permission);
                }
            }

            return permissions;
        }
    }
}