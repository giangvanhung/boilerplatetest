using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;

namespace kamrj.Models.TokenAuth
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AuthenticateModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string UserNameOrEmailAddress { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool RememberClient { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
