using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;

namespace kamrj.Authentication.External
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ExternalAuthManager : IExternalAuthManager, ITransientDependency
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IIocResolver _iocResolver;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ExternalAuthManager(IIocResolver iocResolver, IExternalAuthConfiguration externalAuthConfiguration)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _iocResolver = iocResolver;
            _externalAuthConfiguration = externalAuthConfiguration;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Task<bool> IsValidUser(string provider, string providerKey, string providerAccessCode)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            using (var providerApi = CreateProviderApi(provider))
            {
                return providerApi.Object.IsValidUser(providerKey, providerAccessCode);
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Task<ExternalAuthUserInfo> GetUserInfo(string provider, string accessCode)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            using (var providerApi = CreateProviderApi(provider))
            {
                return providerApi.Object.GetUserInfo(accessCode);
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> CreateProviderApi(string provider)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var providerInfo = _externalAuthConfiguration.Providers.FirstOrDefault(p => p.Name == provider);
            if (providerInfo == null)
            {
                throw new Exception("Unknown external auth provider: " + provider);
            }

            var providerApi = _iocResolver.ResolveAsDisposable<IExternalAuthProviderApi>(providerInfo.ProviderApiType);
            providerApi.Object.Initialize(providerInfo);
            return providerApi;
        }
    }
}
