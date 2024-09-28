using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai1.First_Approach
{
    public class ProvinceService : IRepository<Province>
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public ProvinceService(APITeachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Province>> GetListAsync()
        {
            return await _context.Provinces.ToListAsync();
        }

        public async Task<IEnumerable<Province>> SearchAsync(Expression<Func<Province, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Provinces;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Addresses)
                .Select(x => new Province
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProvinceCode = x.ProvinceCode,
                    AxisMeridian = x.AxisMeridian,
                    CountryId = x.CountryId,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Addresses = x.Addresses.Select(y => new Address
                    {
                        Id = y.Id,
                        AddressText = y.AddressText,
                        CountryId = y.CountryId,
                        ProvinceId = y.ProvinceId,
                        DistrictId = y.DistrictId,
                        WardId = y.WardId,
                        TownId = y.TownId,
                        Latitude = y.Latitude,
                        Longitude = y.Longitude,
                        Notes = y.Notes,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Province> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Provinces;

            if (exportDTO is true)
                return await ctx.FindAsync(id);

            return await ctx
                .Include(x => x.Addresses)
                .Select(x => new Province
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProvinceCode = x.ProvinceCode,
                    AxisMeridian = x.AxisMeridian,
                    CountryId = x.CountryId,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Addresses = x.Addresses.Select(y => new Address
                    {
                        Id = y.Id,
                        AddressText = y.AddressText,
                        CountryId = y.CountryId,
                        ProvinceId = y.ProvinceId,
                        DistrictId = y.DistrictId,
                        WardId = y.WardId,
                        TownId = y.TownId,
                        Latitude = y.Latitude,
                        Longitude = y.Longitude,
                        Notes = y.Notes,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Province> CreateAsync(Province entity)
        {
            var province = _mapper.Map<Province>(entity);
            await _context.Provinces.AddAsync(province);
            await _context.SaveChangesAsync();
            return province;
        }

        public async Task<Province> UpdateAsync(Province entity)
        {
            var mappedEntity = _mapper.Map<Province>(entity);
            _context.Provinces.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var province = _context.Provinces.Find(id);
            if (province is null)
                return 0;

            _context.Provinces.Remove(province);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Provinces.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Provinces.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Provinces.Any(x => x.Id == id);
        }
    }
}
