using AutoMapper;
using Healio.Models.DTO;
using System;

namespace Healio.Models.MappingProfiles
{
    public class DoctorMapping : Profile
    {
        public DoctorMapping()
        {
            CreateMap<DoctorProfile, PublicDoctorDTO>();
        }
    }
}
