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
    public class TeamService : ICrudService<Team>
    {
        private readonly ICrudGrpcService<Team> _grpcService;

        public TeamService(ICrudGrpcService<Team> grpcService)
        {
            _grpcService = grpcService;
        }
        
        public async Task<ActionResponse> AddAsync(Team model)
        {
            return await _grpcService.AddAsync(model);
        }
        
        public async Task<ActionResponse> UpdateAsync(Dictionary<string, object> model)
        {
            if (model.TryGetValue("status", out var status))
            {
                var teamNewStatus = int.Parse(status.ToString());
                if (!(teamNewStatus == (int)TeamStatus.Deleted || teamNewStatus == (int)TeamStatus.Lost))
                    return ActionResponse.InvalidStatus($"{TeamStatus.Deleted.ToString()}, {TeamStatus.Lost.ToString()}");
            }
            var id = Guid.Parse(model["id"].ToString());
            var team = await _grpcService.GetByIdAsync(id);
            if (team.Id != id) return ActionResponse.NotFound("Team");
            
            UpdateObjectByReflection.SetProperties(model, team);
            return await _grpcService.UpdateAsync(team);
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _grpcService.GetAllAsync();
        }

        public async Task<Team> GetByIdAsync(Guid id)
        {
            return await _grpcService.GetByIdAsync(id);
        }
    }
}