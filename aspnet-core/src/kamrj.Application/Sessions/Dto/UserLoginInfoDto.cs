using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using kamrj.Authorization.Users;

namespace kamrj.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UserLoginInfoDto : EntityDto<long>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Surname { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string UserName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string EmailAddress { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
