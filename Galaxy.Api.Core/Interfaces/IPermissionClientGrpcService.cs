using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IPermissionClientGrpcService
    {
        Task<ActionResponse> RemovePermissions(IEnumerable<Permission> permissions);
        Task<ActionResponse> AddPermissions(IEnumerable<Permission> permissions);
        Task<List<UserPermission>> GetPermissionsAsync(int userId);
    }
}