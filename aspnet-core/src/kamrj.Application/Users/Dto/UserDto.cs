using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using kamrj.Authorization.Users;

namespace kamrj.Users.Dto
{
    [AutoMapFrom(typeof(User))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UserDto : EntityDto<long>
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
        public string FullName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public DateTime? LastLoginTime { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public DateTime CreationTime { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string[] RoleNames { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
