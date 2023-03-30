using AutoMapper;
using ps_project_api.Api.Models.DoctorDTOs;
using ps_project_api.Business.UpdateModels;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.DonorDTOs;
using ps_project_api.Models.Enums;

namespace ProiectPS.Api.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile() 
        {
            CreateMap<Doctor, DoctorViewDTO>()
                .ForMember(dest => dest.Email, map => map.MapFrom(src => src.User.Email));

            CreateMap<DoctorCreateDTO, Doctor>()
                .ForPath(dest => dest.User.Email, map => map.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.Password, map => map.MapFrom(src => src.Password))
                .ForPath(dest => dest.User.AccountTypeId, map => map.MapFrom(src => 2));

            CreateMap<DoctorUpdateDTO, Doctor>()
                .ForPath(dest => dest.User.Email, map => map.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.Password, opt => opt.Ignore())
                .ForPath(dest => dest.User.AccountTypeId, map => map.MapFrom(src => AccountType.DOCTOR));
        }
    }
}
