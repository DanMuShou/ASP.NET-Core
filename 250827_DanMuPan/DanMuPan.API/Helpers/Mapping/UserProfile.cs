using AutoMapper;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Entities;
using DanMu.Pan.MediatR.Commands.User;

namespace DanMuPan.API.Helpers.Mapping;

// TODO : 未完成代码

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<AddUserCommand, User>();
    }
}
