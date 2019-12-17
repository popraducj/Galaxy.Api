using System.Threading.Tasks;
using Galaxy.Api.Core.Models.UserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IUserService
    {
        Task ValidateAsync(TokenValidatedContext ctx);
        Task<UserActionResponse> ActivateAsync(string token);
        Task<UserActionResponse> RegisterAsync(UserRegister model);
        Task<UserActionResponse> UpdateAsync(UserUpdate model);
        Task<UserActionResponse> ChangePasswordAsync(UserChangePassword model);
        Task<UserLoginActionResponse> LoginAsync(UserLogin model);
    }
}