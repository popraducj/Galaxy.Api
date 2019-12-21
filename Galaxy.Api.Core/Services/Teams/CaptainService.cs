using System;
using System.Collections.Generic;
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
        
        public Task<UserActionResponse> UpdateAsync(Captain captain)
        {
            return _captainGrpcService.UpdateAsync(captain);
        }

        public async Task<List<Captain>> GetAllAsync()
        {
            return await _captainGrpcService.GetAllAsync();
        }

        public async Task<Captain> GetByIdAsync(Guid id)
        {
            return await _captainGrpcService.GetByIdAsync(id);
        }
    }
}