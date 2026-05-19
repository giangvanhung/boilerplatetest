using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace kamrj.MultiTenancy.Dto
{
    [AutoMapFrom(typeof(Tenant))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TenantDto : EntityDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(AbpTenantBase.TenancyNameRegex)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string TenancyName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [StringLength(AbpTenantBase.MaxNameLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; }        
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool IsActive {get; set;}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
