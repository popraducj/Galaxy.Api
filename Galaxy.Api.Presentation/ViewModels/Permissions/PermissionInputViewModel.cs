using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Permissions
{
    public class PermissionInputViewModel : InputObjectGraphType<Permission>
    {
        public PermissionInputViewModel()
        {
            Field(x => x.UserId);
            Field(x => x.UserPermission);
        }
    }
}