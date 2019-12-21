using System.Threading.Tasks;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IUserClientGrpcService
    {
        Task<int> GetUserIdAsync(string username);
        Task<UserActionResponse> ActivateAsync(string token);
        Task<UserActionResponse> RegisterAsync(UserRegister model);
        Task<UserActionResponse> UpdateAsync(UserUpdate model);
        Task<UserActionResponse> ChangePasswordAsync(UserChangePassword model);
        Task<string> LoginAsync(UserLogin model);
    }
}