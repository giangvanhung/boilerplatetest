using Abp.Application.Services.Dto;

namespace kamrj.MultiTenancy.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PagedTenantResultRequestDto : PagedResultRequestDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Keyword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool? IsActive { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}

