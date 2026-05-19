using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using kamrj.MultiTenancy;

namespace kamrj.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TenantLoginInfoDto : EntityDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string TenancyName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
