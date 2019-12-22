using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Helpers;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Services
{
    public class CaptainService : ICrudService<Captain>
    {
        private readonly ICrudGrpcService<Captain> _grpcService;

        public CaptainService(ICrudGrpcService<Captain> grpcService)
        {
            _grpcService = grpcService;
        }
        
        public async Task<ActionResponse> AddAsync(Captain model)
        {
            return await _grpcService.AddAsync(model);
        }
        
        public async Task<ActionResponse> UpdateAsync(Dictionary<string, object> model)
        {
            var id = Guid.Parse(model["id"].ToString());
            var captain = await _grpcService.GetByIdAsync(id);
            if (captain.Id != id) return ActionResponse.NotFound("Captain");
            
            UpdateObjectByReflection.SetProperties(model, captain);
            if(captain.Status == CaptainStatus.HasTeam || captain.Status == CaptainStatus.Unassigned)
                return ActionResponse.InvalidStatus($"{CaptainStatus.Deleted.ToString()}, {CaptainStatus.Unknown.ToString()}," +
                                                    $" {CaptainStatus.Retired.ToString()}");
            return await _grpcService.UpdateAsync(captain);
        }

        public async Task<List<Captain>> GetAllAsync()
        {
            return await _grpcService.GetAllAsync();
        }

        public async Task<Captain> GetByIdAsync(Guid id)
        {
            return await _grpcService.GetByIdAsync(id);
        }
    }
}