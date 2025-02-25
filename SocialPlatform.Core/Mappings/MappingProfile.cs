using AutoMapper;
using SocialPlatform.Core.DTOs;
using SocialPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SocialPlatform.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserResponseDto>();
            CreateMap<UserRequestDto, User>();

            CreateMap<LoginRequestDto, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));


            CreateMap<Message, MessageResponseDto>();
            CreateMap<MessageRequestDto, Message>();

            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
