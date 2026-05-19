using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using kamrj.Roles.Dto;
using kamrj.Users.Dto;

namespace kamrj.Users
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task DeActivate(EntityDto<long> user);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task Activate(EntityDto<long> user);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<ListResultDto<RoleDto>> GetRoles();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task ChangeLanguage(ChangeUserLanguageDto input);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<bool> ChangePassword(ChangePasswordDto input);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
