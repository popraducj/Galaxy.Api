using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Services
{
    public class CaptainService : ICaptainService
    {
        private readonly ICaptainGrpcService _captainGrpcService;

        public CaptainService(ICaptainGrpcService captainGrpcService)
        {
            _captainGrpcService = captainGrpcService;
        }
        
        public Task<UserActionResponse> AddAsync(Captain captain)
        {
            return _captainGrpcService.AddAsync(captain);
        }
    }
}