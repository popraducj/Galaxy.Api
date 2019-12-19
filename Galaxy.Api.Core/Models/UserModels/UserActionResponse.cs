using System.Collections.Generic;

namespace Galaxy.Api.Core.Models.UserModels
{
    public class UserActionResponse
    {
        public UserActionResponse()
        {
            Errors = new List<UserActionError>();
        }
        public bool Success { get; set; }
        public List<UserActionError> Errors { get; set; }
    }
}