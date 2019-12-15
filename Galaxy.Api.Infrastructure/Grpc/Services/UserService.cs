using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Auth.Grpc;
using Grpc.Net.Client;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class UserService: IUserClientGrpcService
    {
        public async Task<int> VerifyIfUserExistsAsync(string username)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new User.UserClient(channel);

            var replay = await client.VerifyUserAsync(new UserRequest
            {
                Username = username
            });
            return replay.Id;
        }
    }
}