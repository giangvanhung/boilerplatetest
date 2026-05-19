using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using kamrj.Authorization.Roles;

namespace kamrj.Roles.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class CreateRoleDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        
        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string DisplayName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string NormalizedName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        
        [StringLength(Role.MaxDescriptionLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Description { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public List<string> GrantedPermissions { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public CreateRoleDto()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            GrantedPermissions = new List<string>();
        }
    }
}
