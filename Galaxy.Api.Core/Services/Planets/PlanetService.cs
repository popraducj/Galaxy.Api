using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Enums.Planets;
using Galaxy.Api.Core.Helpers;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.Planets;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Services.Planets
{
    public class PlanetService: ICrudService<Planet>
    {
        private readonly ICrudGrpcService<Planet> _grpcService;

        public PlanetService(ICrudGrpcService<Planet> grpcService)
        {
            _grpcService = grpcService;
        }
        
        public async Task<ActionResponse> AddAsync(Planet model)
        {
            return await _grpcService.AddAsync(model);
        }
        
        public async Task<ActionResponse> UpdateAsync(Dictionary<string, object> model)
        {
            if (model.TryGetValue("status", out var status))
            {
                var planetNewStatus = int.Parse(status.ToString());
                if (!(planetNewStatus == (int) PlanetStatus.EnRoute ||
                      planetNewStatus == (int) PlanetStatus.NotExplored))
                    return ActionResponse.InvalidStatus(
                        $"{PlanetStatus.EnRoute.ToString()}, {PlanetStatus.NotExplored.ToString()},");
            }
            var id = Guid.Parse(model["id"].ToString());
            var planet = await _grpcService.GetByIdAsync(id);
            if (planet.Id != id) return ActionResponse.NotFound("Planet");
            
            UpdateObjectByReflection.SetProperties(model, planet);
            return await _grpcService.UpdateAsync(planet);
        }

        public async Task<List<Planet>> GetAllAsync()
        {
            return await _grpcService.GetAllAsync();
        }

        public async Task<Planet> GetByIdAsync(Guid id)
        {
            return await _grpcService.GetByIdAsync(id);
        }
    }
}