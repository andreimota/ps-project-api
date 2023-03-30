using AutoMapper;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.TransfusionCenterDTOs;

namespace ps_project_api.Profiles
{
    public class TransfusionCenterProfile : Profile
    {
        public TransfusionCenterProfile()
        {
            CreateMap<TransfusionCenter, TransfusionCenterViewDTO>();
        }
    }
}
