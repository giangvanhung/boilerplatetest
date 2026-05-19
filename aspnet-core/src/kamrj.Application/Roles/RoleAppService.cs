using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using kamrj.Authorization;
using kamrj.Authorization.Roles;
using kamrj.Authorization.Users;
using kamrj.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace kamrj.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override async Task<RoleDto> CreateAsync(CreateRoleDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override async Task<RoleDto> UpdateAsync(RoleDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override async Task DeleteAsync(EntityDto<int> input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList()
            ));
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override IQueryable<Role> CreateFilteredQuery(PagedRoleResultRequestDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Repository.GetAllIncluding(x => x.Permissions)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.DisplayName.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override async Task<Role> GetEntityByIdAsync(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedRoleResultRequestDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return query.OrderBy(r => r.DisplayName);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected virtual void CheckErrors(IdentityResult identityResult)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            identityResult.CheckErrors(LocalizationManager);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
    }
}

