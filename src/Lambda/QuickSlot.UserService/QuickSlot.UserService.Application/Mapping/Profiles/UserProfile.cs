using AutoMapper;
using QuickSlot.UserService.Application.CQRS.Commands;
using QuickSlot.UserService.Domain.Entities;
using QuickSlot.UserService.Shared.DTOs;


namespace QuickSlot.UserService.Application.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<CreateUserCommand, User>().ReverseMap();
        }
    }
}
