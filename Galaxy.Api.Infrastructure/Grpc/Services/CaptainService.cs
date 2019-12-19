using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Teams.Presentation;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class CaptainService : ICaptainGrpcService
    {
        private readonly ILogger<CaptainService> _logger;
        private readonly Captain.CaptainClient _client;

        public CaptainService(ILogger<CaptainService> logger)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress("https://localhost:5005");
            _client = new Captain.CaptainClient(channel);
        }

        public async Task<UserActionResponse> AddAsync(Core.Models.Teams.Captain captain)
        {
            var result = await _client.AddCaptainAsync(new AddCaptainRequest
            {
                Age = captain.Age,
                Username = captain.Username
            });
            return ToActionResponse(result);
        }

        private static UserActionResponse ToActionResponse(CaptainActionReplay replay)
        {
            var response = new UserActionResponse
            {
                Success = replay.Success
            };

            if (replay.Success) return response;
            
            foreach (var error in replay.Errors)
            {
                replay.Errors.Add(new ActionError
                {
                    Code = error.Code,
                    Description = error.Description
                });
            }

            return response;
        }
        
    }
}