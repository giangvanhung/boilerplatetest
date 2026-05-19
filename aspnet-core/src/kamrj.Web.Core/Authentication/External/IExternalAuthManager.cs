using System.Threading.Tasks;

namespace kamrj.Authentication.External
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IExternalAuthManager
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<bool> IsValidUser(string provider, string providerKey, string providerAccessCode);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task<ExternalAuthUserInfo> GetUserInfo(string provider, string accessCode);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
