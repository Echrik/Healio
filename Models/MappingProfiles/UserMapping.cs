using AutoMapper;
using Healio.Models.DTO;
using System;

namespace Healio.Models.MappingProfiles
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, PublicUserDTO>();
        }
    }
}
