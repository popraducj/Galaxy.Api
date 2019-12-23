using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Enums.Planets;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Infrastructure.Helpers;
using Galaxy.Planets;
using Galaxy.Teams;
using Galaxy.Teams.Core.Models.Settings;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class ExplorationService: ICrudGrpcService<Core.Models.Planets.Exploration>
    {
        private readonly ILogger<ExplorationService> _logger;
        private readonly Exploration.ExplorationClient _client;

        public ExplorationService(ILogger<ExplorationService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress(appSettings.Value.Urls.PlanetsUrl);
            _client = new Exploration.ExplorationClient(channel);
        }

        public async Task<ActionResponse> AddAsync(Core.Models.Planets.Exploration exploration)
        {
            var result = await _client.AddAsync(exploration.ToExplorationModel());
            return result.ToActionResponse();
        }
        
        public Task<ActionResponse> UpdateAsync(Core.Models.Planets.Exploration exploration)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Core.Models.Planets.Exploration>> GetAllAsync()
        {
            var explorations = new List<Core.Models.Planets.Exploration>();
            using (var call = _client.GetAll(new Empty()))
            {
                var responseStream = call.ResponseStream;
                await foreach (var explorationModel in responseStream.ReadAllAsync())
                {
                    explorations.Add(explorationModel.ToExploration());
                }
            }

            return explorations;
        }

        public async Task<Core.Models.Planets.Exploration> GetByIdAsync(Guid id)
        {
            var explorationModel = await _client.GetByIdAsync(new IdRequest
            {
                Id = id.ToString()
            });
            return explorationModel.ToExploration();
        }
    }
}