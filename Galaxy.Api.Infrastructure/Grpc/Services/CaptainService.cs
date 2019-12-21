using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Enums;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Teams.Presentation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using ActionError = Galaxy.Teams.Presentation.ActionError;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class CaptainService : ICaptainGrpcService
    {
        private readonly ILogger<CaptainService> _logger;
        private readonly Captain.CaptainClient _client;

        public CaptainService(ILogger<CaptainService> logger)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress("https://localhost:5005");
            _client = new Captain.CaptainClient(channel);
        }

        public async Task<UserActionResponse> AddAsync(Core.Models.Teams.Captain captain)
        {
            var result = await _client.AddCaptainAsync(new AddCaptainRequest
            {
                Age = captain.Age,
                Username = captain.Username
            });
            return ToActionResponse(result);
        }
        
        public async Task<UserActionResponse> UpdateAsync(Core.Models.Teams.Captain captain)
        {
            var result = await _client.UpdateCaptainAsync(new UpdateCaptainRequest
            {
                Id = captain.Id.ToString(),
                Status = (int)captain.Status
            });
            return ToActionResponse(result);
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
            var captain = await _client.GetByIdAsync(new CaptainIdRequest
            {
                Id = id.ToString()
            });
            return ToCaptain(captain);
        }

        private static Core.Models.Teams.Captain ToCaptain(CaptainReplay captain)
        {
            if (captain == null) return null;
            return new Core.Models.Teams.Captain
            {
                 Age = captain.Age,
                 Expeditions = captain.Expeditions,
                 Id = Guid.Parse(captain.Id),
                 Status = (CaptainStatus)captain.Status,
                 Name = captain.Name
            };
        }

        private static UserActionResponse ToActionResponse(CaptainActionReplay replay)
        {
            var response = new UserActionResponse
            {
                Success = replay.Success
            };

            if (replay.Success) return response;
            
            foreach (var error in replay.Errors)
            {
                replay.Errors.Add(new ActionError
                {
                    Code = error.Code,
                    Description = error.Description
                });
            }

            return response;
        }
        
    }
}