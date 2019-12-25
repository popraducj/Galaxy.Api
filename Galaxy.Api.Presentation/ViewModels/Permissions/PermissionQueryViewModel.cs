using Galaxy.Api.Core.Models.UserModels;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.ViewModels.Permissions
{
    public class PermissionQueryViewModel: ObjectGraphType<Permission>
    {
        public PermissionQueryViewModel()
        {
            Field(x => x.UserId);
            Field(x => x.UserPermission);
        }
    }
}