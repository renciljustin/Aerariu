using Aerariu.API.Dtos;
using Aerariu.API.Lib.Middleware;
using Aerariu.Core.Models;
using AutoMapper;

namespace Aerariu.API.Lib.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile() {
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(obj => Guid.NewGuid()));
            //.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(obj => obj.UserRoles.Select(role => new UserRole { RoleId = role })));

            CreateMap<User, UserInfo>();
        }
    }
}
