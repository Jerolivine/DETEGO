using AutoMapper;
using Detego.WebAPI.Dto;
using Detego.WebAPI.Models;

namespace Detego.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, SystemUser>();
            CreateMap<Store, GetStoresDto>();
        }
    }
}
