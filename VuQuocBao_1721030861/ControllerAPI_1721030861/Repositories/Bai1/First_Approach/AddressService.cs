using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai1.First_Approach
{
    public class AddressService : IRepository<Address>
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public AddressService(APITeachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Address>> GetListAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<IEnumerable<Address>> SearchAsync(Expression<Func<Address, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Addresses;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Country)
                .Include(x => x.District)
                .Include(x => x.Province)
                .Include(x => x.Ward)
                .Select(x => new Address
                {
                    Id = x.Id,
                    AddressText = x.AddressText,
                    CountryId = x.CountryId,
                    ProvinceId = x.ProvinceId,
                    DistrictId = x.DistrictId,
                    WardId = x.WardId,
                    TownId = x.TownId,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Notes = x.Notes,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    Country = new Country
                    {
                        Id = x.Country.Id,
                        Name = x.Country.Name,
                        CountryCode = x.Country.CountryCode,
                        Status = x.Country.Status,
                        CreatedAt = x.Country.CreatedAt,
                        CreatedBy = x.Country.CreatedBy,
                        UpdatedAt = x.Country.UpdatedAt,
                        UpdatedBy = x.Country.UpdatedBy,
                    },
                    District = new District
                    {
                        Id = x.District.Id,
                        Name = x.District.Name,
                        DistrictCode = x.District.DistrictCode,
                        ProvinceId = x.District.ProvinceId,
                        Status = x.District.Status,
                        CreatedAt = x.District.CreatedAt,
                        CreatedBy = x.District.CreatedBy,
                        UpdatedAt = x.District.UpdatedAt,
                        UpdatedBy = x.District.UpdatedBy,
                    },
                    Province = new Province
                    {
                        Id = x.Province.Id,
                        Name = x.Province.Name,
                        ProvinceCode = x.Province.ProvinceCode,
                        AxisMeridian = x.Province.AxisMeridian,
                        CountryId = x.Province.CountryId,
                        Status = x.Province.Status,
                        CreatedAt = x.Province.CreatedAt,
                        CreatedBy = x.Province.CreatedBy,
                        UpdatedAt = x.Province.UpdatedAt,
                        UpdatedBy = x.Province.UpdatedBy,
                    },
                    Ward = new Ward
                    {
                        Id = x.Ward.Id,
                        Name = x.Ward.Name,
                        WardCode = x.Ward.WardCode,
                        DistrictId = x.Ward.DistrictId,
                        Status = x.Ward.Status,
                        CreatedAt = x.Ward.CreatedAt,
                        CreatedBy = x.Ward.CreatedBy,
                        UpdatedAt = x.Ward.UpdatedAt,
                        UpdatedBy = x.Ward.UpdatedBy,
                    }
                })
                .ToListAsync();
        }

        public async Task<Address> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Addresses;

            if (exportDTO is true)
                return await ctx.FindAsync(id);

            return await ctx
                .Include(x => x.Country)
                .Include(x => x.District)
                .Include(x => x.Province)
                .Include(x => x.Ward)
                .Select(x => new Address
                {
                    Id = x.Id,
                    AddressText = x.AddressText,
                    CountryId = x.CountryId,
                    ProvinceId = x.ProvinceId,
                    DistrictId = x.DistrictId,
                    WardId = x.WardId,
                    TownId = x.TownId,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Notes = x.Notes,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    Country = new Country
                    {
                        Id = x.Country.Id,
                        Name = x.Country.Name,
                        CountryCode = x.Country.CountryCode,
                        Status = x.Country.Status,
                        CreatedAt = x.Country.CreatedAt,
                        CreatedBy = x.Country.CreatedBy,
                        UpdatedAt = x.Country.UpdatedAt,
                        UpdatedBy = x.Country.UpdatedBy,
                    },
                    District = new District
                    {
                        Id = x.District.Id,
                        Name = x.District.Name,
                        DistrictCode = x.District.DistrictCode,
                        ProvinceId = x.District.ProvinceId,
                        Status = x.District.Status,
                        CreatedAt = x.District.CreatedAt,
                        CreatedBy = x.District.CreatedBy,
                        UpdatedAt = x.District.UpdatedAt,
                        UpdatedBy = x.District.UpdatedBy,
                    },
                    Province = new Province
                    {
                        Id = x.Province.Id,
                        Name = x.Province.Name,
                        ProvinceCode = x.Province.ProvinceCode,
                        AxisMeridian = x.Province.AxisMeridian,
                        CountryId = x.Province.CountryId,
                        Status = x.Province.Status,
                        CreatedAt = x.Province.CreatedAt,
                        CreatedBy = x.Province.CreatedBy,
                        UpdatedAt = x.Province.UpdatedAt,
                        UpdatedBy = x.Province.UpdatedBy,
                    },
                    Ward = new Ward
                    {
                        Id = x.Ward.Id,
                        Name = x.Ward.Name,
                        WardCode = x.Ward.WardCode,
                        DistrictId = x.Ward.DistrictId,
                        Status = x.Ward.Status,
                        CreatedAt = x.Ward.CreatedAt,
                        CreatedBy = x.Ward.CreatedBy,
                        UpdatedAt = x.Ward.UpdatedAt,
                        UpdatedBy = x.Ward.UpdatedBy,
                    }
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Address> CreateAsync(Address entity)
        {
            var address = _mapper.Map<Address>(entity);
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address> UpdateAsync(Address entity)
        {
            var mappedEntity = _mapper.Map<Address>(entity);
            _context.Addresses.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var address = _context.Addresses.Find(id);
            if (address is null)
                return 0;

            _context.Addresses.Remove(address);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Addresses.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Addresses.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Addresses.Any(x => x.Id == id);
        }
    }
}
