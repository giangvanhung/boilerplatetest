using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Authorization;

namespace kamrj.Roles.Dto
{
    [AutoMapFrom(typeof(Permission))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PermissionDto : EntityDto<long>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string DisplayName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Description { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
