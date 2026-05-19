using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using kamrj.Sessions.Dto;

namespace kamrj.Sessions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class SessionAppService : kamrjAppServiceBase, ISessionAppService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [DisableAuditing]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            return output;
        }
    }
}
