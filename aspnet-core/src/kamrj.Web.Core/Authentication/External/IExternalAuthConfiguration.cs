using System.Collections.Generic;

namespace kamrj.Authentication.External
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IExternalAuthConfiguration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        List<ExternalLoginProviderInfo> Providers { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
