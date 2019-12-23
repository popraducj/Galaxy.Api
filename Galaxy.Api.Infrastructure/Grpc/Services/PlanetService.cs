using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class PlanetService: ICrudGrpcService<Core.Models.Planets.Planet>
    {
        private readonly ILogger<PlanetService> _logger;
        private readonly Planet.PlanetClient _client;

        public PlanetService(ILogger<PlanetService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress(appSettings.Value.Urls.PlanetsUrl);
            _client = new Planet.PlanetClient(channel);
        }

        public async Task<ActionResponse> AddAsync(Core.Models.Planets.Planet planet)
        {
            var result = await _client.AddAsync(planet.ToPlanetModel());
            return result.ToActionResponse();
        }
        
        public async Task<ActionResponse> UpdateAsync(Core.Models.Planets.Planet planet)
        {
            var result = await _client.UpdateAsync(planet.ToPlanetModel());
            return result.ToActionResponse();
        }

        public async Task<List<Core.Models.Planets.Planet>> GetAllAsync()
        {
            var planets = new List<Core.Models.Planets.Planet>();
            using (var call = _client.GetAll(new Empty()))
            {
                var responseStream = call.ResponseStream;
                await foreach (var planetModel in responseStream.ReadAllAsync())
                {
                    planets.Add(planetModel.ToPlanet());
                }
            }

            return planets;
        }

        public async Task<Core.Models.Planets.Planet> GetByIdAsync(Guid id)
        {
            var planetModel = await _client.GetByIdAsync(new IdRequest
            {
                Id = id.ToString()
            });
            return planetModel.ToPlanet();
        }
    }
}