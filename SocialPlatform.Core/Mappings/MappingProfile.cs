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
            
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<UserRequestDto, User>();

            
            CreateMap<Message, MessageResponseDto>();
            CreateMap<MessageRequestDto, Message>();
        }
    }
}
