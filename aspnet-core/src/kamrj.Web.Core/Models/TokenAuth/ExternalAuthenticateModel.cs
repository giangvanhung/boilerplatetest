using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace kamrj.Models.TokenAuth
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ExternalAuthenticateModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [StringLength(UserLogin.MaxLoginProviderLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string AuthProvider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [StringLength(UserLogin.MaxProviderKeyLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string ProviderKey { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string ProviderAccessCode { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
