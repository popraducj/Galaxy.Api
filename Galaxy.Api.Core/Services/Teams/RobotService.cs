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
    public class RobotService: ICrudService<Robot>
    {
        private readonly ICrudGrpcService<Robot> _grpcService;

        public RobotService(ICrudGrpcService<Robot> grpcService)
        {
            _grpcService = grpcService;
        }
        
        public async Task<ActionResponse> AddAsync(Robot model)
        {
            return await _grpcService.AddAsync(model);
        }
        
        public async Task<ActionResponse> UpdateAsync(Dictionary<string, object> model)
        {
            var id = Guid.Parse(model["id"].ToString());
            var robot = await _grpcService.GetByIdAsync(id);
            
            if (model.TryGetValue("status", out var status))
            {
                var robotNewStatus = (RobotStatus) int.Parse(status.ToString());
                if (robotNewStatus == RobotStatus.Assigned || robotNewStatus == RobotStatus.Unassigned)
                    return ActionResponse.InvalidStatus($"{RobotStatus.On.ToString()}, {RobotStatus.Off.ToString()}," +
                                                        $" {RobotStatus.Exploring.ToString()}, {RobotStatus.Deleted.ToString()}" +
                                                        $", {RobotStatus.Broken.ToString()}");
                if(robotNewStatus == RobotStatus.Deleted && robot.Status != RobotStatus.Unassigned)
                    return ActionResponse.CantDelete(nameof(Robot));
            }
            
            if (robot.Id != id) return ActionResponse.NotFound("Robot");
            
            UpdateObjectByReflection.SetProperties(model, robot);
            return await _grpcService.UpdateAsync(robot);
        }

        public async Task<List<Robot>> GetAllAsync()
        {
            return await _grpcService.GetAllAsync();
        }

        public async Task<Robot> GetByIdAsync(Guid id)
        {
            return await _grpcService.GetByIdAsync(id);
        }
    }
}