using System.Collections.Generic;

namespace kamrj.Roles.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class GetRoleForEditOutput
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public RoleEditDto Role { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public List<FlatPermissionDto> Permissions { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public List<string> GrantedPermissionNames { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}