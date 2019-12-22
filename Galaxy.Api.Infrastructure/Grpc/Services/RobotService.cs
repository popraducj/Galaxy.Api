using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Api.Infrastructure.Helpers;
using Galaxy.Robots.Presentation;
using Galaxy.Teams.Core.Models.Settings;
using Galaxy.Teams.Presentation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class RobotService : ICrudGrpcService<Core.Models.Teams.Robot>
    {
        private readonly ILogger<RobotService> _logger;
        private readonly Robot.RobotClient _client;

        public RobotService(ILogger<RobotService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress(appSettings.Value.Urls.TeamsUrl);
            _client = new Robot.RobotClient(channel);
        }

        public async Task<ActionResponse> AddAsync(Core.Models.Teams.Robot robot)
        {
            var result = await _client.AddAsync(robot.ToRobotModel());
            return result.ToActionResponse();
        }
        
        public async Task<ActionResponse> UpdateAsync(Core.Models.Teams.Robot robot)
        {
            var result = await _client.UpdateAsync(robot.ToRobotModel());
            return result.ToActionResponse();
        }

        public async Task<List<Core.Models.Teams.Robot>> GetAllAsync()
        {
            var robots = new List<Core.Models.Teams.Robot>();
            using (var call = _client.GetAll(new Empty()))
            {
                var responseStream = call.ResponseStream;
                await foreach (var robotModel in responseStream.ReadAllAsync())
                {
                    robots.Add(robotModel.ToRobot());
                }
            }

            return robots;
        }

        public async Task<Core.Models.Teams.Robot> GetByIdAsync(Guid id)
        {
            var robotModel = await _client.GetByIdAsync(new IdRequest
            {
                Id = id.ToString()
            });
            return robotModel.ToRobot();
        }
    }
}