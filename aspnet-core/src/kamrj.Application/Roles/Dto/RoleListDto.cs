using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace kamrj.Roles.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class RoleListDto : EntityDto, IHasCreationTime
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string DisplayName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool IsStatic { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool IsDefault { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public DateTime CreationTime { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
