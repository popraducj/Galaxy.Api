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
    public class ShuttleService: ICrudService<Shuttle>
    {
        private readonly ICrudGrpcService<Shuttle> _grpcService;

        public ShuttleService(ICrudGrpcService<Shuttle> grpcService)
        {
            _grpcService = grpcService;
        }
        
        public async Task<ActionResponse> AddAsync(Shuttle model)
        {
            return await _grpcService.AddAsync(model);
        }
        
        public async Task<ActionResponse> UpdateAsync(Dictionary<string, object> model)
        {
            if (model.TryGetValue("status", out var status))
            {
                var shuttleNewStatus = int.Parse(status.ToString());
                if (shuttleNewStatus == (int) ShuttleStatus.Assigned || shuttleNewStatus == (int)ShuttleStatus.Unassigned)
                    return ActionResponse.InvalidStatus($"{ShuttleStatus.Deleted.ToString()}, {ShuttleStatus.Broken.ToString()}," +
                                                        $" {ShuttleStatus.Off.ToString()}, {ShuttleStatus.On.ToString()}");
            }
            
            var id = Guid.Parse(model["id"].ToString());
            var shuttle = await _grpcService.GetByIdAsync(id);
            if (shuttle.Id != id) return ActionResponse.NotFound("Shuttle");
            
            UpdateObjectByReflection.SetProperties(model, shuttle);
            return await _grpcService.UpdateAsync(shuttle);
        }

        public async Task<List<Shuttle>> GetAllAsync()
        {
            return await _grpcService.GetAllAsync();
        }

        public async Task<Shuttle> GetByIdAsync(Guid id)
        {
            return await _grpcService.GetByIdAsync(id);
        }
    }
}