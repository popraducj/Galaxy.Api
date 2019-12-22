using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Infrastructure.Helpers;
using Galaxy.Teams.Core.Models.Settings;
using Galaxy.Teams.Presentation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class CaptainService : ICrudGrpcService<Core.Models.Teams.Captain>
    {
        private readonly ILogger<CaptainService> _logger;
        private readonly Captain.CaptainClient _client;

        public CaptainService(ILogger<CaptainService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress(appSettings.Value.Urls.TeamsUrl);
            _client = new Captain.CaptainClient(channel);
        }

        public async Task<ActionResponse> AddAsync(Core.Models.Teams.Captain captain)
        {
            var result = await _client.AddAsync(new CaptainModel
            {
                Age = captain.Age,
                Username = captain.Username
            });
            return result.ToActionResponse();
        }
        
        public async Task<ActionResponse> UpdateAsync(Core.Models.Teams.Captain captain)
        {
            var result = await _client.UpdateAsync(new CaptainModel
            {
                Id = captain.Id.ToString(),
                Status = (int)captain.Status
            });
            return result.ToActionResponse();
        }

        public async Task<List<Core.Models.Teams.Captain>> GetAllAsync()
        {
            var captains = new List<Core.Models.Teams.Captain>();
            using (var call = _client.GetAll(new Empty()))
            {
                var responseStream = call.ResponseStream;
                await foreach (var captain in responseStream.ReadAllAsync())
                {
                    captains.Add(ToCaptain(captain));
                }
            }

            return captains;
        }

        public async Task<Core.Models.Teams.Captain> GetByIdAsync(Guid id)
        {
            var captain = await _client.GetByIdAsync(new IdRequest
            {
                Id = id.ToString()
            });
            return ToCaptain(captain);
        }

        private static Core.Models.Teams.Captain ToCaptain(CaptainModel captain)
        {
            if(string.IsNullOrEmpty(captain.Id)) return new Core.Models.Teams.Captain();
            return new Core.Models.Teams.Captain
            {
                 Age = captain.Age,
                 Expeditions = captain.Expeditions,
                 Id = Guid.Parse(captain.Id),
                 Status = (CaptainStatus)captain.Status,
                 Name = captain.Name
            };
        }
    }
}