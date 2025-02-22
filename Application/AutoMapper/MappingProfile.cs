using Application.CQRS.Users.Handlers;
using Application.CQRS.Users.ResponseDtos;
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Remove.Command, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());

        CreateMap<User, RegisterDto>();
    }
}
