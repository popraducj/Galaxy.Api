using Galaxy.Api.Core.Enums;

namespace Galaxy.Api.Core.Models.UserModels
{
    public class Permission
    {
        public int  UserId { get; set; }
        public UserPermission UserPermission { get; set; }
    }
}