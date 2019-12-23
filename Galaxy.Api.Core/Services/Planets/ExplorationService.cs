using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Helpers;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Services.Planets
{
    public class ExplorationService: ICrudService<Exploration>
    {
        private readonly ICrudGrpcService<Exploration> _grpcService;

        public ExplorationService(ICrudGrpcService<Exploration> grpcService)
        {
            _grpcService = grpcService;
        }
        
        public async Task<ActionResponse> AddAsync(Exploration model)
        {
            return await _grpcService.AddAsync(model);
        }
        
        public async Task<ActionResponse> UpdateAsync(Dictionary<string, object> model)
        {
            var id = Guid.Parse(model["id"].ToString());
            var exploration = await _grpcService.GetByIdAsync(id);
            if (exploration.Id != id) return ActionResponse.NotFound("Exploration");
            
            UpdateObjectByReflection.SetProperties(model, exploration);
            return await _grpcService.UpdateAsync(exploration);
        }

        public async Task<List<Exploration>> GetAllAsync()
        {
            return await _grpcService.GetAllAsync();
        }

        public async Task<Exploration> GetByIdAsync(Guid id)
        {
            return await _grpcService.GetByIdAsync(id);
        }
    }
}