using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models.UserModels;
using Galaxy.Auth.Grpc;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Infrastructure.Grpc.Services
{
    public class UserService: IUserClientGrpcService
    {
        private readonly ILogger<UserService> _logger;
        private readonly User.UserClient _client;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
            
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            _client = new User.UserClient(channel);
        }
        
        public async Task<int> GetUserIdAsync(string username)
        {
            var replay = await _client.GetUserAsync(new UserRequest
            {
                Username = username
            });
            return replay.Id;
        }
        
        public async Task<UserActionResponse> ActivateAsync(string token)
        {
            try
            {
                var replay = await _client.ActivateAsync(new TokenModel
                {
                    Token = token
                });
                
                return ToActionResponse(replay);
            }
            catch
            {
                _logger.LogError("User activation has failed");
                return GetUnauthorizedAccessError();
            }
        }

        public async Task<UserActionResponse> RegisterAsync(UserRegister model)
        {
            try
            {
                var replay = await _client.RegisterAsync(new RegisterRequest
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = model.Password
                });
                
                return ToActionResponse(replay);
            }
            catch
            {
                _logger.LogError("User registration has failed");
                return GetUnauthorizedAccessError();
            }
        }

        public async Task<UserActionResponse> UpdateAsync(UserUpdate model)
        {
            
            try
            {
                var replay = await _client.UpdateAsync(new UpdateRequest
                {
                    Username = model.Username,
                    Name = model.Name,
                    Phone = model.Phone
                });
                
                return ToActionResponse(replay);
            }
            catch
            {
                _logger.LogError("User update has failed");
                return GetUnauthorizedAccessError();
            }
        }

        public async Task<UserActionResponse> ChangePasswordAsync(UserChangePassword model)
        {
            try
            {
                var replay = await _client.ChangePasswordAsync(new ChangePasswordRequest
                {
                    Username = model.Username,
                    OldPassword = model.OldPassword,
                    NewPassword = model.NewPassword
                });
                
                return ToActionResponse(replay);
            }
            catch
            {
                _logger.LogError("User change password has failed");
                return GetUnauthorizedAccessError();
            }
        }

        public async Task<string> LoginAsync(UserLogin model)
        {
            try
            {
                var replay = await _client.LoginAsync(new LoginRequest
                {
                    Email = model.Email,
                    Password = model.Password
                });

                return replay.Token;
            }
            catch
            {
                _logger.LogError("User login has failed");
                throw new UnauthorizedAccessException();
            }
        }

        private static UserActionResponse ToActionResponse(UserActionReplay protoReplay)
        {
            var response = new UserActionResponse
            {
                Success = protoReplay.Success,
                Errors = new List<UserActionError>()
            };

            foreach (var error in protoReplay.Errors)
            {
                response.Errors.Add(new UserActionError
                {
                    Code = error.Code,
                    Description = error.Description
                });
            }

            return response;
        }

        private static UserActionResponse GetUnauthorizedAccessError()
        {
            return new UserActionResponse
            {
                Success = false,
                Errors = new List<UserActionError>
                {
                    new UserActionError
                    {
                        Code = "UnauthorizedAccess",
                        Description = "You are not authorize to perform this action"
                    }
                }
            };
        }
    }
}