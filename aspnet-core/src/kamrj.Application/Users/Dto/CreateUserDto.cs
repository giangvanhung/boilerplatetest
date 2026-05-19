using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using kamrj.Authorization.Users;

namespace kamrj.Users.Dto
{
    [AutoMapTo(typeof(User))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class CreateUserDto : IShouldNormalize
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string UserName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Surname { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string EmailAddress { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool IsActive { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string[] RoleNames { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Normalize()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            if (RoleNames == null)
            {
                RoleNames = new string[0];
            }
        }
    }
}
