using AutoMapper;
using ps_project_api.Api.Models.DonorDTOs;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.DonorDTOs;
using ps_project_api.Models.Enums;

namespace ps_project_api.Profiles
{
    public class DonorProfile : Profile
    {
        public DonorProfile()
        {
            CreateMap<DonorRegisterDTO, Donor>()
                .ForPath(dest => dest.User.Email, map => map.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.Password, map => map.MapFrom(src => src.Password))
                .ForPath(dest => dest.User.AccountTypeId, map => map.MapFrom(src => AccountType.DONOR));

            CreateMap<Donor, DonorViewDTO>()
                .ForMember(dest => dest.Email, map => map.MapFrom(src => src.User.Email));

            CreateMap<DonorUpdateDTO, Donor>()
                .ForPath(dest => dest.User.Email, map => map.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.Password, opt => opt.Ignore())
                .ForPath(dest => dest.User.AccountTypeId, map => map.MapFrom(src => AccountType.DONOR));
        }
    }
}
