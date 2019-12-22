using System.Threading.Tasks;
using Galaxy.Api.Core.Models.UserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IUserService
    {
        Task ValidateAsync(TokenValidatedContext ctx);
        Task<ActionResponse> ActivateAsync(string token);
        Task<ActionResponse> RegisterAsync(UserRegister model);
        Task<ActionResponse> UpdateAsync(UserUpdate model);
        Task<ActionResponse> ChangePasswordAsync(UserChangePassword model);
        Task<LoginActionResponse> LoginAsync(UserLogin model);
    }
}