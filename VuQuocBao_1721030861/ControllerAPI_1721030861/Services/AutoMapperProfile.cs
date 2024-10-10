using AutoMapper;
using ControllerAPI_1721030861.Database.Models;

namespace ControllerAPI_1721030861.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Publisher, PublisherDTO>().ReverseMap();
            CreateMap<Title, TitleDTO>().ReverseMap();
            CreateMap<TitleAuthor, TitleAuthorDTO>().ReverseMap();
        }
    }
}
