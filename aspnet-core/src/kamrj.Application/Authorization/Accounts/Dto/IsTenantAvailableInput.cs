using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;

namespace kamrj.Authorization.Accounts.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class IsTenantAvailableInput
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string TenancyName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
