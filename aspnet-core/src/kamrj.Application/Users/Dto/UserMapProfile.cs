using AutoMapper;
using kamrj.Authorization.Users;

namespace kamrj.Users.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UserMapProfile : Profile
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public UserMapProfile()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}
