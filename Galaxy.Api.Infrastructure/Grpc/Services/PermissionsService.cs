using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Auth.Grpc;
using Grpc.Core;
using Grpc.Net.Client;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class PermissionsService : IPermissionClientGrpcService
    {
        public async Task<List<UserPermission>> GetPermissions(int userId)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Permissions.PermissionsClient(channel);
            var permissions = new List<UserPermission>();
            using (var call = client.GetPermissions(new UserPermissionRequest{Id = userId}))
            {
                var responseStream = call.ResponseStream;
                StringBuilder responseLog = new StringBuilder("Result: ");

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