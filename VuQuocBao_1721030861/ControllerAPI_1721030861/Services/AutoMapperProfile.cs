using ControllerAPI_1721030861.Database.Models;
using AutoMapper;
//using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Database.Models.Bai3;

namespace ControllerAPI_1721030861.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Bai 1.
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Supplier, SupplierDTO>().ReverseMap();

            // Bai 2 + 3.
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Province, ProvinceDTO>().ReverseMap();
            CreateMap<District, DistrictDTO>().ReverseMap();
            CreateMap<Ward, WardDTO>().ReverseMap();

            // Bai 3.
            CreateMap<Banner, BannerDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Content, ContentDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Portal, PortalDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
        }
    }
}
