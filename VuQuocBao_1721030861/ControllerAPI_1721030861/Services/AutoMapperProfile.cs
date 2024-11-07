using AutoMapper;
using ControllerAPI_1721030861.Database.Models;

namespace ControllerAPI_1721030861.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryApi, CategoryApiDTO>().ReverseMap();
            CreateMap<UserApi, UserApiDTO>().ReverseMap();
            CreateMap<NewsApi, NewsApiDTO>().ReverseMap();
        }
    }
}
