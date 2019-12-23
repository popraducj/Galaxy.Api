using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Infrastructure.Helpers;
using Galaxy.Teams;
using Galaxy.Teams.Core.Models.Settings;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class TeamService: ICrudGrpcService<Core.Models.Teams.Team>
    {
        private readonly ILogger<TeamService> _logger;
        private readonly Team.TeamClient _client;

        public TeamService(ILogger<TeamService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress(appSettings.Value.Urls.TeamsUrl);
            _client = new Team.TeamClient(channel);
        }

        public async Task<ActionResponse> AddAsync(Core.Models.Teams.Team team)
        {
            var result = await _client.AddAsync(team.ToTeamModel());
            return result.ToActionResponse();
        }
        
        public async Task<ActionResponse> UpdateAsync(Core.Models.Teams.Team team)
        {
            var result = await _client.UpdateAsync(team.ToTeamModel());
            return result.ToActionResponse();
        }

        public async Task<List<Core.Models.Teams.Team>> GetAllAsync()
        {
            var teams = new List<Core.Models.Teams.Team>();
            using (var call = _client.GetAll(new Empty()))
            {
                var responseStream = call.ResponseStream;
                await foreach (var teamModel in responseStream.ReadAllAsync())
                {
                    teams.Add(teamModel.ToTeam());
                }
            }

            return teams;
        }

        public async Task<Core.Models.Teams.Team> GetByIdAsync(Guid id)
        {
            var teamModel = await _client.GetByIdAsync(new IdRequest
            {
                Id = id.ToString()
            });
            return teamModel.ToTeam();
        }
    }
}