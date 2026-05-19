using Abp.Authorization;
using kamrj.Authorization.Roles;
using kamrj.Authorization.Users;

namespace kamrj.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
