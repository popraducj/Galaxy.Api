using System.Collections.Generic;

namespace Galaxy.Api.Core.Models.UserModels
{
    public class UserActionResponse
    {
        public bool Success { get; set; }
        public List<UserActionError> Errors { get; set; }
    }
}