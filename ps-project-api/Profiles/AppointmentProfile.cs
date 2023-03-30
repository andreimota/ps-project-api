using AutoMapper;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.AppointmentDTOs;

namespace ps_project_api.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile() 
        {
            CreateMap<Appointment, AppointmentViewDTO>()
                .ForMember(dest => dest.Donor, map => map.MapFrom(src => src.Donor.FirstName + " " + src.Donor.LastName))
                .ForMember(dest => dest.Doctor, map => map.MapFrom(src => src.Doctor.FirstName + " " + src.Doctor.LastName))
                .ForMember(dest => dest.TransfusionCenter, map => map.MapFrom(src => src.TransfusionCenter.Name));

            CreateMap<AppointmentCreateDTO, Appointment>();
        }
    }
}
