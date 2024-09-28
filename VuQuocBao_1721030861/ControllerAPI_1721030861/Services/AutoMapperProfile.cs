using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
//using ControllerAPI_1721030861.Database.Models.Bai2;

namespace ControllerAPI_1721030861.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Bai 1
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<RoleUser, RoleUserDTO>().ReverseMap();
            CreateMap<Shipper, ShipperDTO>().ReverseMap();
            CreateMap<Supplier, SupplierDTO>().ReverseMap();

            // Bai 2
            //CreateMap<Bank, BankDTO>().ReverseMap();
            //CreateMap<BankType, BankTypeDTO>().ReverseMap();
            //CreateMap<Folk, FolkDTO>().ReverseMap();
            //CreateMap<Religion, ReligionDTO>().ReverseMap();
            //CreateMap<School, SchoolDTO>().ReverseMap();

            // Bai 1 + 2
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<District, DistrictDTO>().ReverseMap();
            CreateMap<Province, ProvinceDTO>().ReverseMap();
            CreateMap<Ward, WardDTO>().ReverseMap();
        }
    }
}
