using AutoMapper;
using Healio.Models.DTO;
using System;

namespace Healio.Models.MappingProfiles
{
    public class PatientMapping : Profile
    {
        public PatientMapping()
        {
            CreateMap<PatientProfile, PublicPatientDTO>();
        }
    }
}
