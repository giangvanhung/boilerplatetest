using System.Threading.Tasks;
using Abp.Dependency;

namespace kamrj.Authentication.External
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public abstract class ExternalAuthProviderApiBase : IExternalAuthProviderApi, ITransientDependency
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ExternalLoginProviderInfo ProviderInfo { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Initialize(ExternalLoginProviderInfo providerInfo)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            ProviderInfo = providerInfo;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<bool> IsValidUser(string userId, string accessCode)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var userInfo = await GetUserInfo(accessCode);
            return userInfo.ProviderKey == userId;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public abstract Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
