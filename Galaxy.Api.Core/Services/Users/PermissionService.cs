using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Services.Users
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionClientGrpcService _permissionClientGrpcService;
        private readonly IUserClientGrpcService _userClientGrpcService;

        public PermissionService(IPermissionClientGrpcService permissionClientGrpcService, IUserClientGrpcService userClientGrpcService)
        {
            _permissionClientGrpcService = permissionClientGrpcService;
            _userClientGrpcService = userClientGrpcService;
        }

        public async Task<ActionResponse> AddAsync(IEnumerable<Permission> permissions, string username)
        {
            var currentUserId = await _userClientGrpcService.GetUserIdAsync(username);
            var currentUserPermissions = await _permissionClientGrpcService.GetPermissionsAsync(currentUserId);
            var permissionsList = permissions.Where(permission => currentUserPermissions.Contains(permission.UserPermission)).ToList();
            return await _permissionClientGrpcService.AddPermissions(permissionsList);
        }

        public async Task<ActionResponse> RemoveAsync(IEnumerable<Permission> permissions, string username)
        {
            var currentUserId = await _userClientGrpcService.GetUserIdAsync(username);
            var currentUserPermissions = await _permissionClientGrpcService.GetPermissionsAsync(currentUserId);
            var permissionsList = permissions.Where(permission => currentUserPermissions.Contains(permission.UserPermission)).ToList();
            return await _permissionClientGrpcService.RemovePermissions(permissionsList);
        }

        public async Task<List<Permission>> GetPermissions(int userId)
        {
            var permissions = await _permissionClientGrpcService.GetPermissionsAsync(userId);
            var response = new List<Permission>();
            permissions.ForEach(p => response.Add(new Permission
            {
                UserId = userId,
                UserPermission = p
            }));
            return response;
        }
    }
}