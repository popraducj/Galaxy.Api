using System.Threading.Tasks;

namespace Galaxy.Api.Core.Interfaces
{
    public interface IUserClientGrpcService
    {
        Task<int> VerifyIfUserExistsAsync(string username);
    }
}