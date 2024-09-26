using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai2;

namespace ControllerAPI_1721030861.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Bank, BankDTO>().ReverseMap();
            CreateMap<BankType, BankTypeDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<District, DistrictDTO>().ReverseMap();
            CreateMap<Folk, FolkDTO>().ReverseMap();
            CreateMap<Province, ProvinceDTO>().ReverseMap();
            CreateMap<Religion, ReligionDTO>().ReverseMap();
            CreateMap<School, SchoolDTO>().ReverseMap();
            CreateMap<Ward, WardDTO>().ReverseMap();
        }
    }
}
