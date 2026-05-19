using Abp.Application.Services;
using kamrj.MultiTenancy.Dto;

namespace kamrj.MultiTenancy
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
    }
}

