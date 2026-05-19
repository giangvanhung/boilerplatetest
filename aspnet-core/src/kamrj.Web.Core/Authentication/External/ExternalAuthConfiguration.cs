using System.Collections.Generic;
using Abp.Dependency;

namespace kamrj.Authentication.External
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ExternalAuthConfiguration : IExternalAuthConfiguration, ISingletonDependency
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public List<ExternalLoginProviderInfo> Providers { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ExternalAuthConfiguration()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Providers = new List<ExternalLoginProviderInfo>();
        }
    }
}
