using AutoMapper;
using Healio.Models.DTO;

namespace Healio.Models.MappingProfiles
{
    public class DoctorScheduleMapping : Profile
    {
        public DoctorScheduleMapping()
        {
            CreateMap<DoctorSchedule, DoctorScheduleDTO>();
        }
    }
}
