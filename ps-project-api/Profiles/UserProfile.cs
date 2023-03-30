using AutoMapper;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.User;

namespace ps_project_api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AuthenticateModel, User>();
        }
    }
}
