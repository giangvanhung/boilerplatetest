using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using kamrj.Roles.Dto;

namespace kamrj.Roles
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
