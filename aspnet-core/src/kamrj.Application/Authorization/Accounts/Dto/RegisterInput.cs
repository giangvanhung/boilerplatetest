using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;
using kamrj.Validation;

namespace kamrj.Authorization.Accounts.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class RegisterInput : IValidatableObject
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
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
        [StringLength(AbpUserBase.MaxUserNameLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string UserName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string EmailAddress { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [DisableAuditing]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string CaptchaResponse { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            if (!UserName.IsNullOrEmpty())
            {
                if (!UserName.Equals(EmailAddress) && ValidationHelper.IsEmail(UserName))
                {
                    yield return new ValidationResult("Username cannot be an email address unless it's the same as your email address!");
                }
            }
        }
    }
}
