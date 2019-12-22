using System.Threading.Tasks;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IUserClientGrpcService
    {
        Task<int> GetUserIdAsync(string username);
        Task<ActionResponse> ActivateAsync(string token);
        Task<ActionResponse> RegisterAsync(UserRegister model);
        Task<ActionResponse> UpdateAsync(UserUpdate model);
        Task<ActionResponse> ChangePasswordAsync(UserChangePassword model);
        Task<string> LoginAsync(UserLogin model);
    }
}