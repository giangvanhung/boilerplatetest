using System.Threading.Tasks;
using Abp.Application.Services;
using kamrj.Sessions.Dto;

namespace kamrj.Sessions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface ISessionAppService : IApplicationService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
