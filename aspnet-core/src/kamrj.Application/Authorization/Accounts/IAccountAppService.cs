using System.Threading.Tasks;
using Abp.Application.Services;
using kamrj.Authorization.Accounts.Dto;

namespace kamrj.Authorization.Accounts
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IAccountAppService : IApplicationService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<RegisterOutput> Register(RegisterInput input);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
