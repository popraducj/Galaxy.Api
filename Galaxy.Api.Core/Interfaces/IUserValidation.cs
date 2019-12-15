using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IUserValidationService
    {
        Task ValidateAsync(TokenValidatedContext ctx);
    }
}