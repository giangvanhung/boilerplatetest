using System.Threading.Tasks;

namespace kamrj.Authentication.External
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IExternalAuthProviderApi
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        ExternalLoginProviderInfo ProviderInfo { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<bool> IsValidUser(string userId, string accessCode);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        void Initialize(ExternalLoginProviderInfo providerInfo);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
