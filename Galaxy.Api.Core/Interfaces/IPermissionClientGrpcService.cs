using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Models;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IPermissionClientGrpcService
    {
        Task<List<UserPermission>> GetPermissions(int userId);
    }
}