using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using kamrj.Authorization;
using kamrj.Authorization.Accounts;
using kamrj.Authorization.Roles;
using kamrj.Authorization.Users;
using kamrj.Roles.Dto;
using kamrj.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace kamrj.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public UserAppService(
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override async Task<UserDto> UpdateAsync(UserDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override async Task DeleteAsync(EntityDto<long> input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task Activate(EntityDto<long> user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = true;
            });
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task DeActivate(EntityDto<long> user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = false;
            });
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<ListResultDto<RoleDto>> GetRoles()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task ChangeLanguage(ChangeUserLanguageDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override User MapToEntity(CreateUserDto createInput)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override void MapToEntity(UserDto input, User user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override UserDto MapToEntityDto(User user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override async Task<User> GetEntityByIdAsync(long id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return query.OrderBy(r => r.UserName);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected virtual void CheckErrors(IdentityResult identityResult)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            identityResult.CheckErrors(LocalizationManager);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<bool> ChangePassword(ChangePasswordDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }
            
            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<bool> ResetPassword(ResetPasswordDto input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }
            
            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }
    }
}

