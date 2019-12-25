using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IPermissionService
    {
        Task<ActionResponse> AddAsync(IEnumerable<Permission> permissions, string username);
        Task<ActionResponse> RemoveAsync(IEnumerable<Permission> permissions, string username);
        Task<List<Permission>> GetPermissions(int userId);
    }
}