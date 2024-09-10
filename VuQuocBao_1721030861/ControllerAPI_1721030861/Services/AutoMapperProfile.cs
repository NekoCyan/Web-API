using ControllerAPI_1721030861.Database.Models;
using AutoMapper;

namespace ControllerAPI_1721030861.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<ContentType, M_SelectDropDown>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));
        }
    }
}
