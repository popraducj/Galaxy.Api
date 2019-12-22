using System.Collections.Generic;

namespace Galaxy.Api.Core.Models.UserModels
{
    public class ActionResponse
    {
        public ActionResponse()
        {
            Errors = new List<ActionError>();
        }
        public bool Success { get; set; }
        public List<ActionError> Errors { get; set; }
        
        public static ActionResponse NotFound(string name)
        {
            return new ActionResponse
            {
                Success = false,
                Errors = new List<ActionError>
                {
                    new ActionError
                    {
                        Code = "NotFound",
                        Description = $"{name} was not found"
                    }
                }
            };
        }
        public static ActionResponse InvalidStatus(string valid)
        {
            return new ActionResponse
            {
                Success = false,
                Errors = new List<ActionError>
                {
                    new ActionError
                    {
                        Code = "InvalidStatus",
                        Description = $"You can set status only to {valid}"
                    }
                }
            };
        }
    }
}