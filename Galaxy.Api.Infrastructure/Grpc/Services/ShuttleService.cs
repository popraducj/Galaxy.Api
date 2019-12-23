using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Infrastructure.Helpers;
using Galaxy.Shuttles;
using Galaxy.Teams.Core.Models.Settings;
using Galaxy.Teams;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class ShuttleService : ICrudGrpcService<Core.Models.Teams.Shuttle>
    {
        private readonly ILogger<ShuttleService> _logger;
        private readonly Shuttle.ShuttleClient _client;

        public ShuttleService(ILogger<ShuttleService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress(appSettings.Value.Urls.TeamsUrl);
            _client = new Shuttle.ShuttleClient(channel);
        }

        public async Task<ActionResponse> AddAsync(Core.Models.Teams.Shuttle shuttle)
        {
            var result = await _client.AddAsync(shuttle.ToShuttleModel());
            return result.ToActionResponse();
        }
        
        public async Task<ActionResponse> UpdateAsync(Core.Models.Teams.Shuttle shuttle)
        {
            var result = await _client.UpdateAsync(shuttle.ToShuttleModel());
            return result.ToActionResponse();
        }

        public async Task<List<Core.Models.Teams.Shuttle>> GetAllAsync()
        {
            var shuttles = new List<Core.Models.Teams.Shuttle>();
            using (var call = _client.GetAll(new Empty()))
            {
                var responseStream = call.ResponseStream;
                await foreach (var shuttleModel in responseStream.ReadAllAsync())
                {
                    shuttles.Add(shuttleModel.ToShuttle());
                }
            }

            return shuttles;
        }

        public async Task<Core.Models.Teams.Shuttle> GetByIdAsync(Guid id)
        {
            var shuttleModel = await _client.GetByIdAsync(new IdRequest
            {
                Id = id.ToString()
            });
            return shuttleModel.ToShuttle();
        }
    }
}